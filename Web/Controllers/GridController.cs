using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class GridController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar([DataSourceRequest]DataSourceRequest request)
        {
            string lista = $@"{AppDomain.CurrentDomain.BaseDirectory}lista.json";
            ListaCanais listaCanais = JsonConvert.DeserializeObject<ListaCanais>(lista);
            var resultado = listaCanais.Canal.OrderBy(i => i.Titulo);
            return Json(resultado.ToDataSourceResult(request));
        }
    }
}