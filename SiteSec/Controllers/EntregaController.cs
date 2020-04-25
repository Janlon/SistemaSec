using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SiteSec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    public class EntregaController : Controller
    {
        readonly Api api = new Api();
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<ActionResult> Read([DataSourceRequest]DataSourceRequest request, int ItemId)
        {
            var apiRetorno = await api.Use(HttpMethod.Get, new Entrega(), $"api/Entrega/{ItemId}/Item");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<Entrega> Entregas = JsonConvert.DeserializeObject<List<Entrega>>(str);

            //tratamento da lista
            if (Entregas.Count == 1)
                foreach (var item in Entregas.Where(n => n == null))
                    Entregas = new List<Entrega>();

            apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{Entregas.FirstOrDefault().PessoaId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            Pessoa pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();

            Entregas.FirstOrDefault().Pessoa = pessoa.Nome;

            apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"api/ItemDaOrdemDeServico/{ItemId}");
            str = JsonConvert.SerializeObject(apiRetorno.result);
            ItemOrdemServico itemOrdemServico = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str).FirstOrDefault();

            Entrega Entrega = new Entrega() { ItemDaOrdemDeServicoId = itemOrdemServico.Id };

            foreach (var item in Entregas)
            {
                item.ItemDaOrdemDeServicoId = itemOrdemServico.Id;
            }

            return Json(Entregas.ToDataSourceResult(request));
        }
        public async Task<ActionResult> Create([DataSourceRequest]DataSourceRequest request, Entrega obj, int ItemId)
        {

            if (obj.PessoaId == 0 || ItemId == 0)
                return Json(new[] { new Entrega() }.ToDataSourceResult(request));

            obj.ItemDaOrdemDeServicoId = ItemId;

            var apiRetorno = await api.Use(HttpMethod.Post, obj, "api/Entrega");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Update([DataSourceRequest]DataSourceRequest request, Entrega obj)
        {
            var apiRetorno = await api.Use(HttpMethod.Put, obj, "api/Entrega");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }
        public async Task<ActionResult> Destroy([DataSourceRequest]DataSourceRequest request, int id)
        {
            var apiRetorno = await api.Use(HttpMethod.Delete, new Entrega(), $"api/Entrega/{id}");
            return Json(new[] { apiRetorno }.ToDataSourceResult(request, ModelState));
        }

    }
}