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
    public class GeoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Read(string cep)
        {
            Geo geo = new Geo() { codigo = cep };
            var retorno = await api.UseSimple(HttpMethod.Post, geo);      
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}