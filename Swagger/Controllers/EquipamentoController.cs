using Sec;
using Sec.Business;
using Sec.Models;

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
