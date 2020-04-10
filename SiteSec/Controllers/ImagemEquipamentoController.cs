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
    public class ImagemEquipamentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.EquipamentoId = id == null ? "" : id.ToString();
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string equipamentoId)
        {
            IEnumerable<Imagem> resultado = new List<Imagem>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Imagem(), $"api/Imagem/{equipamentoId}/equipamentos");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Imagem>>(str);
            if (obj != null)
                resultado = obj.OrderBy(p => p.Nome);

            return Json(resultado.ToDataSourceResult(request));
        }

    }
}