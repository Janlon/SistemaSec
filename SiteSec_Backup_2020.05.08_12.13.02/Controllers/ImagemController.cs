using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class ImagemController : Controller
    {
        readonly Api api = new Api();

        public async Task<ActionResult> Index(int Id)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Imagem(), $"api/Imagem/{Id}/");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Imagem>>(str).FirstOrDefault();
            ViewBag.Imagem = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(obj.File));
            return PartialView();
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Imagem obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Imagem");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Imagem(), $"api/Imagem/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

    }
}