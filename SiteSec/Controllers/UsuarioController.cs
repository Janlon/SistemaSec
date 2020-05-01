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
    public class UsuarioController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request)
        {
            var str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Usuario(), "api/Usuario")).result);
            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(str);        
            foreach (var user in usuarios)
            {
                //buscar as informações referente a pessoa
                str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{user.PessoaId}")).result);
                Pessoa pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();
                user.Pessoa = pessoa.Nome;
                user.User = pessoa.Email;

                //buscar as informaçoes referente a permissoes
                List<string> regraName = new List<string>();
                str = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Usuario(), $"api/Usuario/{user.UserId}/Permissoes")).result);
                bool isValid = JsonConvert.DeserializeObject<List<Regra>>(str).Any();
                if (isValid)
                {
                    List<Regra> regras = JsonConvert.DeserializeObject<List<Regra>>(str);
                    foreach (var regra in regras)
                    {
                        regraName.Add(regra.Name);
                    }
                    user.RegraName = regraName;
                } 
            }

            return Json(usuarios.ToDataSourceResult(request));
        }
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, Usuario obj)
        {
            var apiRetorno = api.Use(HttpMethod.Post, obj, "api/Usuario");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, Usuario obj)
        {
            var apiRetorno = api.Use(HttpMethod.Put, obj, "api/Usuario");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = api.Use(HttpMethod.Delete, new TipoDocumento(), $"api/Usuario/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
    }
}