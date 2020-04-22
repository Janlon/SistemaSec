using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.EquipamentoId = equipamentoId;

            var Imagens = new List<Imagem>();

            var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{equipamentoId}/Imagens");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();

            if (equipamento != null)
                Imagens = equipamento.Imagens;
            
            return Json(Imagens.ToDataSourceResult(request));
        }

        public async Task<ActionResult> Upload(IEnumerable<HttpPostedFileBase> files, int id)
        {
            if (files != null && id > 0)
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