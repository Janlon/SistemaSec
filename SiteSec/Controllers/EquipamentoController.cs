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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace SiteSec.Controllers
{
    public class EquipamentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            IEnumerable<Equipamento> resultado = new List<Equipamento>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Descricao);

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Equipamento obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Equipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Equipamento obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Equipamento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Equipamento(), $"api/Equipamento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> ViewQrCode(int Id)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{Id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();

            return PartialView(obj);
        }
        public async Task<JsonResult> EquipamentosDoSetor(int setorId)
        {
            if (setorId < 1)
                return Json(new[] { new Equipamento() }, JsonRequestBehavior.AllowGet);

            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setorId}/Equipamentos");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


    }
}