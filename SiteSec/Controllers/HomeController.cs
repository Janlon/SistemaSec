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
using System.Security.Principal;
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
                string retirada_descricao = "Sem informações complementares";
                int identificadorItem = 0;
                List<int> pessoas = new List<int>();

                //str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{os.EmpresaId}/EquipamentosDosSetoresDaEmpresa")).result);
                //List<ItemOrdemServico> equipamentosDosSetoresDaEmpresa = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);

                str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"/api/OrdemDeServico/{os.Id}/Itens")).result);
                List<ItemOrdemServico> itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);
                foreach (var item in itens)
                {
                    identificadorItem = item.Id;

                    if (identificadorItem.Equals(0))
                    {
                        //buscar o objeto retirado 
                        str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Retirada(), $"api/Retirada/{identificadorItem}/Item")).result);
                        Retirada retirada = JsonConvert.DeserializeObject<List<Retirada>>(str).FirstOrDefault();
                        retirada_descricao = retirada.Descricao;
                        pessoas.Add(retirada.PessoaId);

                    }
                }
                Agenda agenda = new Agenda()
                {
                    OrdemId = os.Id,
                    Pessoas = pessoas,
                    EmpresaId = os.EmpresaId,
                    Title = os.Numero,
                    Description = retirada_descricao,
                    Start = DateTime.SpecifyKind(os.Emissao, DateTimeKind.Local),
                    End = DateTime.SpecifyKind(os.Validade, DateTimeKind.Local)
                };
                eventos.Add(agenda);
            }
            return Json(eventos.ToDataSourceResult(request, ModelState));
        }
        public async Task<JsonResult> Create([DataSourceRequest]DataSourceRequest request, Agenda obj)
        {
            ApiRetorno apiRetorno = new ApiRetorno();

            if (string.IsNullOrEmpty(obj.Itens))
                return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));

            //buscar o objeto ordem de serviço
            apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            OrdemServico ordemServico = JsonConvert.DeserializeObject<List<OrdemServico>>(str).LastOrDefault();
            if(ordemServico == null)
            {
                return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
            }

            //cadastrar a ordem de serviço
            OrdemServico os = new OrdemServico()
            {
                EmpresaId = obj.EmpresaId,
                Numero = Convert.ToString(Convert.ToInt32(ordemServico.Numero.Trim()) + 1),
                Emissao = DateTime.SpecifyKind(obj.Start, DateTimeKind.Local),
                Validade = DateTime.SpecifyKind(obj.End, DateTimeKind.Local),
            };
            apiRetorno = await api.Use(HttpMethod.Post, os, "api/ordemservico");
            // pegar o valor do id retornado
            string retorno = JsonConvert.SerializeObject(apiRetorno.origin);
            OrdemServico os_retorno = JsonConvert.DeserializeObject<OrdemServico>(retorno);

            foreach (var (equipamentoId, servicoId) in from itens in obj.Itens.Split(',')
                                                       let item = itens.Split(';')
                                                       let equipamentoId = Helpers.ApenasNumeros(item[1]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[1]))
                                                       let servicoId = Helpers.ApenasNumeros(item[2]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[2]))
                                                       select (equipamentoId, servicoId))
            {

                //buscar o objeto equipamento
                apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{equipamentoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();
               
                //buscar o objeto servicos
                apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{servicoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();
                
                ItemOrdemServico item = new ItemOrdemServico
                {
                    OrdemDeServicoId = os_retorno.Id,
                    EquipamentoId = equipamentoId,
                    ServicoId = servicoId 
                };

                apiRetorno = await api.Use(HttpMethod.Post, item, "api/ItemDaOrdemDeServico");
                // pegar o valor do id retornado
                retorno = JsonConvert.SerializeObject(apiRetorno.origin);
                ItemOrdemServico item_retorno = JsonConvert.DeserializeObject<ItemOrdemServico>(retorno);
                //gravar  o responsavel pela retirada
                foreach (var pessoa in obj.Pessoas)
                {
                    Retirada retirada = new Retirada
                    {
                        Execucao = DateTime.SpecifyKind(obj.Start, DateTimeKind.Local),
                        Descricao = string.IsNullOrEmpty(obj.Description) ? "Sem informações complementares" : obj.Description,
                        PessoaId = pessoa,
                        ItemDaOrdemDeServicoId = item_retorno.Id
                    };
                    apiRetorno = await api.Use(HttpMethod.Post, retirada, "api/Retirada");
                }        
            }

            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

    }
}