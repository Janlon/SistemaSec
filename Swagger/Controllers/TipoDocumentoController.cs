using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{

    public class TipoDocumentoController : ApiController
    {

        public CrudResult<TipoDeDocumento> Get()
        {
            return Engine.TiposDeDocumentos.List();
        }

        public CrudResult<TipoDeDocumento> Get(int id)
        {
            return Engine.TiposDeDocumentos.Find(new object[] { id });
        }

        public CrudResult<TipoDeDocumento> Post(TipoDeDocumento obj)
        {
            return Engine.TiposDeDocumentos.Insert(obj);
        }

        public CrudResult<TipoDeDocumento> Put(TipoDeDocumento obj)
        {
            return Engine.TiposDeDocumentos.Update(obj);
        }

        public CrudResult<TipoDeDocumento> Delete(int id)
        {
            return Engine.TiposDeDocumentos.Delete(Engine.TiposDeDocumentos.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
