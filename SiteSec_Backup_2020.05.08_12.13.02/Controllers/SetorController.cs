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
using SiteSec.Models.Consumo;

namespace SiteSec.Controllers
{
    public class SetorController : Controller
    {
        internal Api Api = new Api();

        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            //trazendo o objeto setor
            var apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            foreach (var item in setores)
            {
                //trazendo o objeto empresa 
                apiRetorno = await Api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{item.EmpresaId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                item.Empresa = empresa.RazaoSocial;

                //trazendo o objeto tipo de setor
                apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{item.TipoDeSetorId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoSetor tipoSetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                item.Sigla = tipoSetor.Sigla;
                item.Descricao = tipoSetor.Descricao;
            }

            return Json(setores.ToDataSourceResult(request));
        }       
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Setor obj)
        {
            ApiRetorno apiRetorno = new ApiRetorno();
            foreach (var item in obj.TiposDeSetores)
            {
                Setor s = new Setor()
                {
                    TipoDeSetorId = item.Id,
                    EmpresaId = obj.EmpresaId
                };

               apiRetorno = await Api.Use(HttpMethod.Post, s, "api/Setor");
            }

            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Setor obj)
        {
            var apiRetorno = await Api.Use(HttpMethod.Put, obj, "api/Setor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await Api.Use(HttpMethod.Delete, new Setor(), $"api/Setor/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<JsonResult> SetoresDaEmpresa(int empresaId)
        {
            if (empresaId < 1)
                return Json(new[] { new Setor() }, JsonRequestBehavior.AllowGet);

            var apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{empresaId}/setores");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            List<TipoSetor> ts = new List<TipoSetor>();
            if (setores != null)
            {
                foreach (var item in setores)
                {
                    //trazendo o objeto tipo de setor
                    apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{item.TipoDeSetorId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    TipoSetor tipoSetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                    ts.Add(tipoSetor);
                }
            }

            return Json(ts, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> EquipamentosDosSetoresDaEmpresa(int empresaId)
        {
            ItemOrdemServico itemOrdemServico = new ItemOrdemServico();
            if (empresaId < 1)
                return Json(new[] { itemOrdemServico }, JsonRequestBehavior.AllowGet);

            //buscar o objeto servicos --- executa 1 unica vez
            var apiRetorno = await Api.Use(HttpMethod.Get, new Servico(), $"api/Servico/");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Servico> servicos = JsonConvert.DeserializeObject<List<Servico>>(str);

            //buscar o objeto setores da empresa --- executa 1 unica vez
            apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{empresaId}/setores");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            List<ItemSetor> itensSetores = new List<ItemSetor>();

            //laço dos setores
            foreach (var setor in setores)
            {
                //buscar o objeto tipo de setores
                apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{setor.TipoDeSetorId}/");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoSetor tiposetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                //buscar o ojeto equipamento que esta em cada setor
                apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setor.Id}/Equipamentos");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                List<Equipamento> equipamentos = JsonConvert.DeserializeObject<List<Equipamento>>(str);

                List<ItemEquipamento> itensEquipamentos = new List<ItemEquipamento>();

                //laço do equipamentos
                foreach (var equipamento in equipamentos)
                {
                    //buscar o ojeto tipo de equipamento
                    apiRetorno = await Api.Use(HttpMethod.Get, new Setor(), $"api/TipoEquipamento/{equipamento.TipoEquipamentoId}/");
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
