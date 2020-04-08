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
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            IEnumerable<Setor> resultado = new List<Setor>();
            var apiRetorno = api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Setor>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult SetoresDaEmpresa([DataSourceRequest]DataSourceRequest request, int id)
        {
            if (id < 1)
                return Json(new[] { new Setor() }.ToDataSourceResult(request));

            var apiRetorno = api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{id}/setores");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Setor>>(str);

            return Json(obj.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Setor obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Setor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Setor obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Setor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Setor(), $"api/Setor/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}
