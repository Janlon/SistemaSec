using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class DocumentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? 0 : id;
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IEnumerable<Documento> resultado = new List<Documento>();
            var apiRetorno = api.Use(HttpMethod.Get, new Documento(), $"api/Documento/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Documento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Id);

            return Json(resultado.ToDataSourceResult(request));
        }

        public ActionResult DocumentosPorPessoa([DataSourceRequest]DataSourceRequest request, int id)
        {
            if(id < 1)
                return Json(new[] { new Documento() }.ToDataSourceResult(request));

            IEnumerable<Documento> resultado = new List<Documento>();
            var apiRetorno = api.Use(HttpMethod.Get, new Documento(), $"api/Documento/{id}/documentos");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Documento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Id);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Documento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Documento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Documento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Documento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Documento(), $"api/Documento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}