using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SiteSec.Controllers
{
    public partial class HomeController : Controller
    {   
        private readonly Api api = new Api();
        protected override void Initialize(RequestContext requestContext)
        {
            if (!string.IsNullOrEmpty(requestContext.HttpContext.Request["culture"]))
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(requestContext.HttpContext.Request["culture"]);
            }
            base.Initialize(requestContext);
        }
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
                        OrdemId = item.OrdemDeServicoId,
                        PessoaId = pessoa.Id,
                        ItemId = item.Id,
                        EmpresaId = empresa.Id,
                        Title = os.Numero,
                        Description = item.Serviço + " no " + item.Equipamento + "\r\n" + "Empresa: " + item.Empresa + "\r\n" + "Setor: " + item.Setor,
                        Start = DateTime.SpecifyKind(os.Emissao, DateTimeKind.Utc),
                        End = DateTime.SpecifyKind(os.Validade, DateTimeKind.Utc)
                    };
                    eventos.Add(agenda);
                }
            }
            return Json(eventos.ToDataSourceResult(request, ModelState));
        }
        public async Task<JsonResult> Create([DataSourceRequest]DataSourceRequest request, Agenda obj)
        {
            ApiRetorno apiRetorno = new ApiRetorno();
            var ListaResultados = "";
            if (string.IsNullOrEmpty(ListaResultados))
                return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));

            var colecao = ListaResultados.Split(',');
            foreach (var (equipamentoId, servicoId) in from itens in colecao
                                                       let item = itens.Split(';')
                                                       let equipamentoId = Helpers.ApenasNumeros(item[1]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[1]))
                                                       let servicoId = Helpers.ApenasNumeros(item[2]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[2]))
                                                       select (equipamentoId, servicoId))
            {

                //buscar o objeto equipamento
                apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{equipamentoId}");
                var str = JsonConvert.SerializeObject(apiRetorno.result);
                Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();
                //buscar o objeto servicos
                apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{servicoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();
                ItemOrdemServico itemOrdemServico = new ItemOrdemServico()
                {
                    EquipamentoId = equipamentoId,
                    ServicoId = servicoId
                };

                apiRetorno = await api.Use(HttpMethod.Post, itemOrdemServico, "api/ItemDaOrdemDeServico");
            }
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

    }
}