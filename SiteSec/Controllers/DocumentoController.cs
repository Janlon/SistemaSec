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
    public class DocumentoController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int id)
        {
            ViewBag.PessoaId = id;
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, int PessoaId)
        {
            try
            {
                //trazendo o objeto "documento"
                var apiRetorno = await api.Use(HttpMethod.Get, new Documento(), $"api/Documento/{PessoaId}/documentos");
                var str = JsonConvert.SerializeObject(apiRetorno.result);
                var documentos = JsonConvert.DeserializeObject<List<Documento>>(str);

                foreach (var item in documentos)
                {
                    //trazendo o objeto "tipo de documento"
                    apiRetorno = await api.Use(HttpMethod.Get, new TipoDocumento(), $"api/TipoDocumento/{item.TipoDeDocumentoId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    var tipodocumento = JsonConvert.DeserializeObject<List<TipoDocumento>>(str).FirstOrDefault();
                    
                    item.Sigla = tipodocumento.Sigla;
                    item.Descricao = tipodocumento.Descricao;
                    item.Identificador = tipodocumento.Identificador;
                }

                return Json(documentos.ToDataSourceResult(request));
            }
            catch (Exception)
            {
                return Json(new[] { new Documento() }.ToDataSourceResult(request));
            }
          
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Documento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Documento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Documento obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Documento");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new Documento(), $"api/Documento/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}