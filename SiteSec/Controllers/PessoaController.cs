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
    public class PessoaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> ViewUpload(int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Pessoa>>(str);
            return PartialView(obj.FirstOrDefault());
        }

        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            List<Pessoa> resultado = new List<Pessoa>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Pessoa>>(str);
            if (obj.Count > 0)
                resultado = obj.OrderBy(p => p.Nome).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Pessoa obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Pessoa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Pessoa obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Pessoa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Pessoa(), $"api/Pessoa/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}