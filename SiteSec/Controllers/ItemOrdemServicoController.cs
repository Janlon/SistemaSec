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
    public class ItemOrdemServicoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.OrdemServicoId = id == null ? 0 : id;
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, int id)
        {
           
            var apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);

            return Json(obj.ToDataSourceResult(request));
        }


        public async Task<ActionResult> Itens([DataSourceRequest]DataSourceRequest request, int OrdemServicoId)
        {
            ViewBag.OrdemServicoId = OrdemServicoId;

            var apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"/api/OrdemDeServico/{OrdemServicoId}/Itens");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);

            return Json(itens.ToDataSourceResult(request));
        }
        

        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, ItemOrdemServico obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/ItemDaOrdemDeServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, ItemOrdemServico obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/ItemDaOrdemDeServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}