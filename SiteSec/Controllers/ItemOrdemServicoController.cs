using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SiteSec.Models;
using System.Threading.Tasks;
using System.Activities.Expressions;

namespace SiteSec.Controllers
{
    public class ItemOrdemServicoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.OrdemServicoId = id == null ? 0 : id;
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, int id)
        {
           
            var apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);

            return Json(obj.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Itens([DataSourceRequest]DataSourceRequest request, int OrdemServicoId)
        {
            ViewBag.OrdemServicoId = OrdemServicoId;

            var apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"/api/OrdemDeServico/{OrdemServicoId}/Itens");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);

            return Json(itens.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, string ListaResultados)
        {

            ApiRetorno apiRetorno = new ApiRetorno();

            if(string.IsNullOrEmpty(ListaResultados))
                return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));


            //apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{1}");
            //var str = JsonConvert.SerializeObject(apiRetorno.result);
            //OrdemServico ordemServico = JsonConvert.DeserializeObject<List<OrdemServico>>(str).FirstOrDefault();

           // List<ItemOrdemServico> items = new List<ItemOrdemServico>();
            
            var colecao = ListaResultados.Split(',');
            foreach (var (equipamentoId, servicoId) in from itens in colecao
                                                                let item = itens.Split(';')
                                                               // let setorId = Helpers.ApenasNumeros(item[0]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[0]))
                                                                let equipamentoId = Helpers.ApenasNumeros(item[1]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[1]))
                                                                let servicoId = Helpers.ApenasNumeros(item[2]) == "" ? 0 : Convert.ToInt32(Helpers.ApenasNumeros(item[2]))
                                                                select (equipamentoId, servicoId))
            {
                ////buscar o objeto Setor
                //apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setorId}");
                //str = JsonConvert.SerializeObject(apiRetorno.result);
                //Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault();
                //buscar o objeto equipamento
                //apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{equipamentoId}");
                //str = JsonConvert.SerializeObject(apiRetorno.result);
                //Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();
                ////buscar o objeto servicos
                //apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{servicoId}");
                //str = JsonConvert.SerializeObject(apiRetorno.result);
                //Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();
                ItemOrdemServico itemOrdemServico = new ItemOrdemServico()
                {
                    OrdemDeServicoId = 1,
                    // OrdemDeServico = ordemServico,
                    EquipamentoId = equipamentoId,
                    //  Equipamento = equipamento,
                    ServicoId = servicoId,
                    //  Servico = servico
                };
               // items.Add(itemOrdemServico);
                apiRetorno = await api.Use(HttpMethod.Post, itemOrdemServico, "api/ItemDaOrdemDeServico");
            }

            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, ItemOrdemServico obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/ItemDaOrdemDeServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<JsonResult> EquipamentosDosSetoresDaEmpresa(int ordemId)
        {
            ItemOrdemServico itemOrdemServico = new ItemOrdemServico();
            if (ordemId < 1)
                return Json(new[] { itemOrdemServico }, JsonRequestBehavior.AllowGet);

            //buscar o objeto da ordem de servico
            var apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{ordemId}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            OrdemServico ordemServico = JsonConvert.DeserializeObject<List<OrdemServico>>(str).FirstOrDefault();

            //buscar o objeto da empresa
            apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{ordemServico.EmpresaId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

           // ordemServico.Empresa = empresa;

            //buscar o objeto setores da empresa
            apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{empresa.Id}/setores");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            //buscar o objeto servicos
            apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Servico> servicos = JsonConvert.DeserializeObject<List<Servico>>(str);

            //buscar o objeto equipamentos dos setores da empresa
            foreach (var setor in setores)
            {
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setor.Id}/Equipamentos");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                List<Equipamento> equipamentos = JsonConvert.DeserializeObject<List<Equipamento>>(str);

                if (equipamentos.Count > 0)
                    foreach (var item in equipamentos)
                    {
                        item.Servicos = servicos;
                    }

                setor.Equipamentos = equipamentos;               
            }

            
            
            return Json(setores, JsonRequestBehavior.AllowGet);
        }
        
    }
}