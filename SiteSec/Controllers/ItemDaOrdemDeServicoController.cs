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
    public class ItemDaOrdemDeServicoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? 0 : id;
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            IEnumerable<ItemDaOrdemDeServico> resultado = new List<ItemDaOrdemDeServico>();
            var apiRetorno = api.ConsumirApi(HttpMethod.Get, new ItemDaOrdemDeServico(), id);
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<ItemDaOrdemDeServico>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Id);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ItemDaOrdemDeServico obj)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Post, obj);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ItemDaOrdemDeServico obj, int id)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Put, obj, id);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Delete, new ItemDaOrdemDeServico(), id);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}