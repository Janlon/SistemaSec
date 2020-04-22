using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class TipoEquipamentoController : ApiController
    {

        public CrudResult<TipoDeEquipamento> Get()
        {
            return Engine.TiposDeEquipamentos.List();
        }

        public CrudResult<TipoDeEquipamento> Get(int id)
        {
            return Engine.TiposDeEquipamentos.Find(new object[] { id });
        }

        public CrudResult<TipoDeEquipamento> Post(TipoDeEquipamento obj)
        {
            return Engine.TiposDeEquipamentos.Insert(obj);
        }

        public CrudResult<TipoDeEquipamento> Put(TipoDeEquipamento obj)
        {
            return Engine.TiposDeEquipamentos.Update(obj);
        }

        public CrudResult<TipoDeEquipamento> Delete(int id)
        {
            return Engine.TiposDeEquipamentos.Delete(Engine.TiposDeEquipamentos.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
