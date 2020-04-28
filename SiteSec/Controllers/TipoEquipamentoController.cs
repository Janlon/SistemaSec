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
    public class TipoEquipamentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request)
        {
            List<TipoEquipamento> resultado = new List<TipoEquipamento>();

            var apiRetorno = await api.Use(HttpMethod.Get, new TipoEquipamento(), "api/TipoEquipamento");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, TipoEquipamento obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/TipoEquipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, TipoEquipamento obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/TipoEquipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new TipoEquipamento(), $"api/TipoEquipamento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}