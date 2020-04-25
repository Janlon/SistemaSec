using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class HomeController : Controller
    {
        readonly Api api = new Api();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Agenda> eventos = new List<Agenda>();
            string str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico")).result);
            List<OrdemServico> ordemServicos = JsonConvert.DeserializeObject<List<OrdemServico>>(str);
            foreach (var os in ordemServicos)
            {
                str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"/api/OrdemDeServico/{os.Id}/Itens")).result);
                List<ItemOrdemServico> itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);
                foreach (var item in itens)
                {
                    //buscar a empresa da ordem de serviço
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{os.EmpresaId}")).result);
                    Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();
                    item.Empresa = empresa.RazaoSocial;

                    //buscar o objeto servicos 
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{item.ServicoId}")).result);
                    Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();
                    item.Serviço = servico.Descricao;

                    //buscar o ojeto equipamento
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{item.EquipamentoId}")).result);
                    Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();

                    //buscar o objeto setor
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{equipamento.SetorId}")).result);
                    Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault();

                    //buscar o objeto tipo de setores
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new TipoSetor(), $"api/TipoSetor/{setor.TipoDeSetorId}")).result);
                    TipoSetor tiposetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();
                    item.Setor = tiposetor.Descricao;

                    //buscar o ojeto tipo de equipamento
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Setor(), $"api/TipoEquipamento/{equipamento.TipoEquipamentoId}")).result);
                    TipoEquipamento tipoEquipamento = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str).FirstOrDefault();
                    item.Equipamento = tipoEquipamento.Descricao;

                    //buscar o objeto retirado 
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Retirada(), $"api/Retirada/{item.Id}/Item")).result);
                    Retirada retirada = JsonConvert.DeserializeObject<List<Retirada>>(str).FirstOrDefault();

                    //buscar o objeto Pessoa
                    str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{retirada.PessoaId}")).result);
                    Pessoa pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();

                    Agenda agenda = new Agenda()
                    {
                        OwerId = pessoa.Id,
                        TaskId = item.Id,
                        Title = "Ordem de Serviço n°: " + os.Numero + " [ " + item.Serviço + " ]",
                        Description = item.Serviço + " no " + item.Equipamento + "\r\n" + "Empresa: " + item.Empresa + "\r\n" + "Setor: " + item.Setor,
                        Start = DateTime.SpecifyKind(os.Emissao, DateTimeKind.Utc),
                        End = DateTime.SpecifyKind(os.Validade, DateTimeKind.Utc),
                        RecurrenceRule = os.Numero
                    };
                    eventos.Add(agenda);
                }
            }
            return Json(eventos.ToDataSourceResult(request, ModelState));
        }
    }
}