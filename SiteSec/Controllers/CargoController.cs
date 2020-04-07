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
    public class CargoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, bool? PessoaFisica = true)
        {
            IEnumerable<Cargo> resultado = new List<Cargo>();
            var apiRetorno = api.ConsumirApiLista(HttpMethod.Get, new Cargo());
            var obj = JsonConvert.DeserializeObject<List<Cargo>>(apiRetorno.mensagem);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao).Where(p => p.PessoaFisica.Equals(PessoaFisica));

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Cargo obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Cargo");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Cargo obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj,  "api/Cargo");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Cargo(), $"api/Cargo/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}
