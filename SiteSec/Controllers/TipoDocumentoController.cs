using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SiteSec.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiteSec.Controllers
{
    public class TipoDocumentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ReadAsync([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<TipoDocumento> resultado = new List<TipoDocumento>();
            var apiRetorno = await api.Use(HttpMethod.Get, new TipoDocumento(), "api/TipoDocumento");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<TipoDocumento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao);

            return Json(resultado.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, TipoDocumento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/TipoDocumento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, TipoDocumento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/TipoDocumento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new TipoDocumento(), $"api/TipoDocumento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}
