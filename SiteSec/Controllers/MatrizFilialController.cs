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
    public class MatrizFilialFilialController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest] DataSourceRequest request, string id)
        {
            var apiRetorno = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new MatrizFilial(), $"api/MatrizFilial/{id}")).result);
            List<MatrizFilial> MatrizesFiliais = JsonConvert.DeserializeObject<List<MatrizFilial>>(apiRetorno);
            return Json(MatrizesFiliais.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest] DataSourceRequest request, MatrizFilial obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/MatrizFilial");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest] DataSourceRequest request, MatrizFilial obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/MatrizFilial");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest] DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new MatrizFilial(), $"api/MatrizFilial/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

    }
}