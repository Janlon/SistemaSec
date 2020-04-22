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
        public ActionResult QrCode(int Id)
        {
            ViewBag.Id = Id;
            return PartialView();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, string id)
        {
           
            //trazendo o objeto equipamento
            var apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{id}");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Equipamento> equipamentos = JsonConvert.DeserializeObject<List<Equipamento>>(str);

            //tratamento da lista
            if (equipamentos.Count == 1)
                foreach (var item in equipamentos.Where(n => n == null))
                   equipamentos = new List<Equipamento>();
                
            foreach (var item in equipamentos.Where(n => n != null))
            {

                //trazendo o objeto setor
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{item.SetorId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault();

                //trazendo o objeto empresa
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{setor.EmpresaId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                item.Empresa = empresa.RazaoSocial;

                //trazendo o objeto tipo de setor
                apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{setor.TipoDeSetorId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoSetor tipoSetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                item.Setor = tipoSetor.Descricao;

                //trazendo o tipo de equipamento
                apiRetorno = await api.Use(HttpMethod.Get, new TipoEquipamento(), $"api/TipoEquipamento/{item.TipoEquipamentoId}");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                TipoEquipamento tipoEquipamento = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str).FirstOrDefault();

                item.Sigla = tipoEquipamento.Sigla;
                item.Descricao = tipoEquipamento.Descricao;
            }
            
            return Json(equipamentos.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Equipamento obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
            
            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Empresa/{obj.EmpresaId}/setores");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault(p => p.TipoDeSetorId.Equals(obj.TipoSetorId));

            foreach (var item in obj.TiposEquipamentos)
            {
                Equipamento equipamento = new Equipamento()
                {
                    EmpresaId = obj.EmpresaId,
                    SetorId = setor.Id,
                    TipoEquipamentoId = item.Id,
                };

                apiRetorno = await api.Use(HttpMethod.Post, equipamento, "api/Equipamento");
            }
             
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
        public async Task<JsonResult> EquipamentosDoSetor(int setorId)
        {
            if (setorId < 1)
                return Json(new[] { new Equipamento() }, JsonRequestBehavior.AllowGet);

            var apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{setorId}/Equipamentos");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            var obj = JsonConvert.DeserializeObject<List<Equipamento>>(str);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}