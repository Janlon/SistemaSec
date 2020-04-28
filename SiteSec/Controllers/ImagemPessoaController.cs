using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
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
    public class ImagemPessoaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.PessoaId = id == null ? "" : id.ToString();
            return View();
        }

        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string pessoaId)
        {
            ViewBag.EquipamentoId = pessoaId;

            var apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{pessoaId}/Imagens");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();

            return Json(obj.Imagens.ToDataSourceResult(request));
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

                        var apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Equipamento/{id}");
                        var str = JsonConvert.SerializeObject(apiRetorno.result);
                        var pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();
                        pessoa.Imagens.Add(imagem);

                        await api.Use(HttpMethod.Put, pessoa, "api/Pessoa");
                    }

                }
            }

            // Return an empty string to signify success.
            return Content("");
        }

    }
}