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
        public async Task<ActionResult> ViewUpload(int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str);
            return PartialView(obj.FirstOrDefault());
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
        public async Task<ActionResult> Upload(IEnumerable<HttpPostedFileBase> files, int id)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] img = ms.GetBuffer();

                        var imagem = new Imagem()
                        {
                            File = img,
                            Nome = file.FileName
                        };

                        var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{id}");
                        var str = JsonConvert.SerializeObject(apiRetorno.result);
                        var equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();
                            equipamento.Imagens.Add(imagem);

                        await api.Use(HttpMethod.Put, equipamento, "api/Equipamento");
                    }
                   
                }
            }

            // Return an empty string to signify success.
            return Content("");
        }

    }
}