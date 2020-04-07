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
    public class EquipamentoDoSetorController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<EquipamentoDoSetor> resultado = new List<EquipamentoDoSetor>();
            var apiRetorno = api.ConsumirApi(HttpMethod.Get, new EquipamentoDoSetor());
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<EquipamentoDoSetor>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.EquipamentoId);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, EquipamentoDoSetor obj)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Post, obj);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, EquipamentoDoSetor obj, int id)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Put, obj, id);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.ConsumirApi(HttpMethod.Delete, new EquipamentoDoSetor(), id);
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}