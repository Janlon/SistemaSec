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
    public class EmpresaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            IEnumerable<Empresa> resultado = new List<Empresa>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Empresa>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.RazaoSocial);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Empresa obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Empresa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Empresa obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Empresa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Empresa(), $"api/Empresa/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}