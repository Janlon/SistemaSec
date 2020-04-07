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
    public class PessoaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            IEnumerable<Pessoa> resultado = new List<Pessoa>();
            var apiRetorno = api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Pessoa>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Nome);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Pessoa obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Pessoa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Pessoa obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Pessoa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Pessoa(), $"api/Pessoa/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}