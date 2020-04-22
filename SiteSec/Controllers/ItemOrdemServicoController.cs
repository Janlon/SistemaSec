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
            List<ItemOrdemServico> itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);
            foreach (var item in itens)
            {
                //buscar objeto ordem de servico
                apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{OrdemServicoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                OrdemServico ordemServico = JsonConvert.DeserializeObject<List<OrdemServico>>(str).FirstOrDefault();

                //peguei o numero da OS
                item.OS = ordemServico.Numero;

                //buscar a empresa da ordem de serviço
                apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{ordemServico.EmpresaId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                item.Empresa = empresa.RazaoSocial;

                //buscar o objeto servicos 
                apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{item.ServicoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();

                item.Serviço = servico.Descricao;

                //buscar o ojeto equipamento
                apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{item.EquipamentoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();

                //buscar o objeto setor
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{equipamento.SetorId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault();

                //buscar o objeto tipo de setores
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{setor.TipoDeSetorId}/");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoSetor tiposetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                item.Setor = tiposetor.Descricao;

                //buscar o ojeto tipo de equipamento
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoEquipamento/{equipamento.TipoEquipamentoId}/");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoEquipamento tipoEquipamento = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str).FirstOrDefault();

                item.Equipamento = tipoEquipamento.Descricao;
            }

            return Json(itens.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, string ListaResultados, int OrdemDeServicoId)
        {
            ApiRetorno apiRetorno = new ApiRetorno();

            if(string.IsNullOrEmpty(ListaResultados))
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
                    OrdemDeServicoId = OrdemDeServicoId,
                    EquipamentoId = equipamentoId,
                    ServicoId = servicoId,
                };

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
            ItemOrdemServico itemOrdemServico =  new ItemOrdemServico();
            if (ordemId < 1)
                return Json(new[] { itemOrdemServico }, JsonRequestBehavior.AllowGet);

            //buscar o objeto servicos --- executa 1 unica vez
            var apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Servico> servicos = JsonConvert.DeserializeObject<List<Servico>>(str);

            //buscar o objeto da ordem de servico --- executa 1 unica vez
            apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{ordemId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            OrdemServico ordemServico = JsonConvert.DeserializeObject<List<OrdemServico>>(str).FirstOrDefault();

            itemOrdemServico.OrdemDeServicoId = ordemServico.Id;

            //buscar o objeto setores da empresa --- executa 1 unica vez
            apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{ordemServico.EmpresaId}/setores");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            List<ItemSetor> itensSetores = new List<ItemSetor>();

            //laço dos setores
            foreach (var setor in setores)
            {
                //buscar o objeto tipo de setores
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{setor.TipoDeSetorId}/");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoSetor tiposetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                //buscar o ojeto equipamento que esta em cada setor
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setor.Id}/Equipamentos");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                List<Equipamento> equipamentos = JsonConvert.DeserializeObject<List<Equipamento>>(str);

                List<ItemEquipamento> itensEquipamentos = new List<ItemEquipamento>();

                //laço do equipamentos
                foreach (var equipamento in equipamentos)
                {
                    //buscar o ojeto tipo de equipamento
                    apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoEquipamento/{equipamento.TipoEquipamentoId}/");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    TipoEquipamento tipoEquipamento = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str).FirstOrDefault();

                    ItemEquipamento itemEquipamento = new ItemEquipamento()
                    {
                        EquipamentoId = equipamento.Id,
                        TipoEquipamentoDescricao = tipoEquipamento.Descricao,
                        Servicos = servicos
                    };

                    itensEquipamentos.Add(itemEquipamento);
  
                }

                ItemSetor itemSetor = new ItemSetor()
                {
                    SetorId = setor.Id,
                    TipoSetorDescricao = tiposetor.Descricao,
                    ItensEquipamentos = itensEquipamentos
                };
                itensSetores.Add(itemSetor);
            }

            itemOrdemServico.ItensOrdemServico = itensSetores;

            return Json(itemOrdemServico, JsonRequestBehavior.AllowGet);
        }
        
    }
}