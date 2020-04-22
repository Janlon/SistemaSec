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
    public class TipoSetorController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            List<TipoSetor> resultado = new List<TipoSetor>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<TipoSetor>>(str);
            if (obj.Count > 0)
                resultado = obj.OrderBy(p => p.Descricao).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, TipoSetor obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/TipoSetor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, TipoSetor obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/TipoSetor");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new TipoSetor(), $"api/TipoSetor/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}