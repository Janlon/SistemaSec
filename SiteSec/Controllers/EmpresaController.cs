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
using System.Text.RegularExpressions;
using SiteSec.Models.Consumo;

namespace SiteSec.Controllers
{
    public class EmpresaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id == null ? "" : id.ToString();
            return View();
        }
        public ActionResult Filial(int? id)
        {
            ViewBag.EmpresaMatrizId = id;
            return PartialView();
        }
        public async Task<JsonResult> Get()
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(str).OrderBy(p => p.RazaoSocial).ToList();

            return Json(empresas, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            List<Empresa> resultado = new List<Empresa>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            resultado = JsonConvert.DeserializeObject<List<Empresa>>(str);
            if (resultado != null)
                resultado = resultado.OrderBy(p => p.RazaoSocial).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Empresa obj)
        {
            Endereco endereco = new Endereco()
            {
                CEP = obj.CEP,
                UF = obj.UF,
                Localidade = obj.Localidade,
                Bairro = obj.Bairro,
                Logradouro = obj.Logradouro,
                Complemento = obj.Complemento
            };
            obj.Endereco = endereco;

            var somenteNumero = string.Join("", Regex.Split(obj.CNPJ, @"[^\d]"));
            obj.CNPJ = somenteNumero;

            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Empresa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Empresa obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Empresa");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, string id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Empresa(), $"api/Empresa/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> ReadFilial([DataSourceRequest]DataSourceRequest request, string id)
        {
            List<Empresa> resultado = new List<Empresa>();
            var apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            resultado = JsonConvert.DeserializeObject<List<Empresa>>(str);
            if (resultado != null)
                resultado = resultado.OrderBy(p => p.RazaoSocial).Where(p => p.Id != Convert.ToInt32(id)).ToList();

            return Json(resultado.ToDataSourceResult(request));
        }
        public async Task<ActionResult> CreateMatrizFilial(EmpresaFilial obj)
        {
            List<Empresa> resultado = new List<Empresa>();
            await api.Use(HttpMethod.Post, obj, "api/Empresa/Filial");
            return Redirect(@Url.Action("Index"));
        }
    }
}