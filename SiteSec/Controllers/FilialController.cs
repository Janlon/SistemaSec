using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class FilialController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest] DataSourceRequest request, string id)
        {
            var apiRetorno = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Filial(), $"api/Filial/{id}")).result);
            List<Filial> Filiais = JsonConvert.DeserializeObject<List<Filial>>(apiRetorno);
            return Json(Filiais.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest] DataSourceRequest request, Filial obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Filial");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest] DataSourceRequest request, Filial obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Filial");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest] DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Filial(), $"api/Filial/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
      
    }
}