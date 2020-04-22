using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{

    public class TipoSetorController : ApiController
    {
        public CrudResult<TipoDeSetor> Get()
        {
            return Engine.TiposDeSetores.List();
        }

        public CrudResult<TipoDeSetor> Get(int id)
        {
            return Engine.TiposDeSetores.Find(new object[] { id });
        }

        public CrudResult<TipoDeSetor> Post(TipoDeSetor obj)
        {
            return Engine.TiposDeSetores.Insert(obj);
        }

        public CrudResult<TipoDeSetor> Put(TipoDeSetor obj)
        {
            return Engine.TiposDeSetores.Update(obj);
        }

        public CrudResult<TipoDeSetor> Delete(int id)
        {
            return Engine.TiposDeSetores.Delete(Engine.TiposDeSetores.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
