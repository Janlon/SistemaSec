using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class UsuarioController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ReadAsync([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<Usuario> resultado = new List<Usuario>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Usuario(), "api/Usuario");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Usuario>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Nome);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Usuario obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Usuario");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Usuario obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Usuario");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new TipoDocumento(), $"api/Usuario/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}