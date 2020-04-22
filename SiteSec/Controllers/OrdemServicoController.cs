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
    public class OrdemServicoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<OrdemServico> ordemServicos = JsonConvert.DeserializeObject<List<OrdemServico>>(str);
            foreach (var item in ordemServicos)
            {
                apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{item.EmpresaId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                item.Empresa = empresa.RazaoSocial;
            }

            return Json(ordemServicos.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, OrdemServico obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/OrdemServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, OrdemServico obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/OrdemServico");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new OrdemServico(), $"api/OrdemServico/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}