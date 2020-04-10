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
    public class OrdemServicoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index( int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> ReadAsync([DataSourceRequest]DataSourceRequest request, string id)
        {
            IEnumerable<OrdemServico> resultado = new List<OrdemServico>();
            var apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<OrdemServico>>(str);
            if (obj != null)
                resultado = obj.OrderByDescending(p=>p.Id);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, OrdemServico obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/OrdemServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, OrdemServico obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/OrdemServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new OrdemServico(), $"api/OrdemServico/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}