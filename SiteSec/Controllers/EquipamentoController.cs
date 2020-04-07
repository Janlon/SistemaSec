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
    public class EquipamentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<Equipamento> resultado = new List<Equipamento>();
            var apiRetorno = api.Use(HttpMethod.Get, new Equipamento(), "api/Equipamento");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Equipamento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Equipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Equipamento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Equipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Equipamento(), $"api/Equipamento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}