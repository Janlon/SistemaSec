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
            //buscar todas as empresas independente se é matriz e filial
            List<Empresa> empresas = new List<Empresa>();
            var apiRetorno = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{id}")).result);
            empresas = JsonConvert.DeserializeObject<List<Empresa>>(apiRetorno);
            if (empresas != null && empresas.Count > 0)
            {
                empresas = empresas.OrderBy(p => p.RazaoSocial).ToList();
                foreach (var empresa in empresas)
                {
                    //remover da lista todas as empresas que são filiais
                    apiRetorno = JsonConvert.SerializeObject((await api.Use(HttpMethod.Get, new EmpresaFilial(), $"api/Empresa/Filial/{empresa.Id}")).result);
                    EmpresaFilial filial = JsonConvert.DeserializeObject<List<EmpresaFilial>>(apiRetorno).FirstOrDefault();
                    if(filial != null)
                        empresas.Remove(empresa);
                }
            }

            return Json(empresas.ToDataSourceResult(request));
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
        public async Task<ActionResult> CreateMatrizFilial([DataSourceRequest]DataSourceRequest request, EmpresaFilial obj)
        {
            //criar a empresa matriz
            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Empresa/Matriz");
            int matrizId = Convert.ToInt32(JsonConvert.SerializeObject(apiRetorno.origin.Id));
            obj.MatrizId = matrizId;

            foreach (var item in obj.Filiais)
            {
                //criar empresa filial.
                obj.EmpresaId = item;
                apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Empresa/Filial");
                int filialId = Convert.ToInt32(JsonConvert.SerializeObject(apiRetorno.origin.Id));
                obj.FilialId = filialId;

                //vincular empresa matriz e filial
                apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Empresa/MatrizFilial");
               
                //atualizar empresa como filial
                apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{item}");
                var str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();
                empresa.EhMatriz = false;
                apiRetorno = await api.Use(HttpMethod.Put, empresa, "api/Empresa");
            }
            return Redirect(Url.Action("Index"));
        }
    }
}