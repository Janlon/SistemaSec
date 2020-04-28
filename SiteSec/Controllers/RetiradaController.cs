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
    public class RetiradaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, int ItemId)
        {    
            var apiRetorno = await api.Use(HttpMethod.Get, new Retirada(), $"api/Retirada/{ItemId}/Item");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Retirada> retiradas = JsonConvert.DeserializeObject<List<Retirada>>(str);

            //tratamento da lista
            if (retiradas.Count == 1)
                foreach (var item in retiradas.Where(n => n == null))
                    retiradas = new List<Retirada>();

            apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{retiradas.FirstOrDefault().PessoaId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            Pessoa pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();

            retiradas.FirstOrDefault().Pessoa = pessoa.Nome;

            apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{ItemId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            ItemOrdemServico itemOrdemServico = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str).FirstOrDefault();

            retiradas.FirstOrDefault().ItemDaOrdemDeServicoId = itemOrdemServico.Id;
      
            return Json(retiradas.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Retirada obj, int ItemId)
        {

          if(obj.PessoaId == 0 || ItemId == 0)
                return Json(new[] { new Retirada() }.ToDataSourceResult(request));

            obj.ItemDaOrdemDeServicoId = ItemId;

            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Retirada");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Retirada obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Retirada");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Retirada(), $"api/Retirada/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
      
    }
}