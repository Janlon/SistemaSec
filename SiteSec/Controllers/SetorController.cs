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

namespace SiteSec.Controllers
{
    public class SetorController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            //trazendo o objeto setor
            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            foreach (var item in setores)
            {
                //trazendo o objeto empresa 
                apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{item.EmpresaId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                item.Empresa = empresa.RazaoSocial;

                //trazendo o objeto tipo de setor
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{item.TipoDeSetorId}");
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

               apiRetorno = await api.Use(HttpMethod.Post, s, "api/Setor");
            }

            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Setor obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Setor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Setor(), $"api/Setor/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<JsonResult> SetoresDaEmpresa(int empresaId)
        {
            if (empresaId < 1)
                return Json(new[] { new Setor() }, JsonRequestBehavior.AllowGet);

            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{empresaId}/setores");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Setor> setores = JsonConvert.DeserializeObject<List<Setor>>(str);

            List<TipoSetor> ts = new List<TipoSetor>();
            if (setores != null)
            {
                foreach (var item in setores)
                {
                    //trazendo o objeto tipo de setor
                    apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{item.TipoDeSetorId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    TipoSetor tipoSetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                    ts.Add(tipoSetor);
                }
            }

            return Json(ts, JsonRequestBehavior.AllowGet);
        }
    }
}
