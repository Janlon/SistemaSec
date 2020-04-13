using Newtonsoft.Json;
using Sec;
using Sec.Business;
using Sec.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class EquipamentoController : ApiController
    {
        public CrudResult<Equipamento> Get()
        {
            return Engine.Equipamentos.List();
        }

        public CrudResult<Equipamento> Get(int id)
        {
            return Engine.Equipamentos.Find(new object[] { id });
        }

        [Route("~/api/Equipamento/{id:int}/qrcode")]
        public CrudResult<Equipamento> GetQrCodeDoEquipamento(int id)
        {
            return Engine.Equipamentos.QrCode(new object[] { id });
        }

        /// <summary>
        /// Retorna a lista de imagens de um equipamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Equipamento/{Id:int}/Imagens")]
        public CrudResult<Equipamento> GetImagensDoEquipamento(int id)
        {         
            var imagens =  Engine.Imagens.Filter(p => p.Equipamentos.FirstOrDefault().Id.Equals(id) ).Result;
            if (imagens.Count < 1)
                return new CrudResult<Equipamento>();

            var equipamentos = Engine.Equipamentos.Find(new object[] { id });
            foreach (var item in equipamentos.Result)
            {
                item.Imagens = imagens;
            }

            return equipamentos;
        }

        public CrudResult<Equipamento> Post(Equipamento obj)
        {
            return Engine.Equipamentos.Insert(obj);
        }

        public CrudResult<Equipamento> Put(Equipamento obj)
        {
            return Engine.Equipamentos.Update(obj);
        }

        public CrudResult<Equipamento> Delete(int id)
        {
            return Engine.Equipamentos.Delete(Engine.Equipamentos.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
