using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class RegraController : Controller
    {

        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            List<Regra> resultado = new List<Regra>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Regra(), $"api/Regra/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            resultado = JsonConvert.DeserializeObject<List<Regra>>(str);
            if (resultado.Count > 0)
                resultado = resultado.OrderBy(p => p.Name).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Regra obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Regra");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Regra obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Regra");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Regra(), $"api/Regra/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}