using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class EntregaController : ApiController
    {
        public CrudResult<EntregaDoItemDaOrdemDeServico> Get()
        {
            return Engine.Entregas.List();
        }

        public CrudResult<EntregaDoItemDaOrdemDeServico> Get(int id)
        {
            return Engine.Entregas.Find(new object[] { id });
        }

        public CrudResult<EntregaDoItemDaOrdemDeServico> Post(EntregaDoItemDaOrdemDeServico obj)
        {
            return Engine.Entregas.Insert(obj);
        }

        public CrudResult<EntregaDoItemDaOrdemDeServico> Put(EntregaDoItemDaOrdemDeServico obj)
        {
            return Engine.Entregas.Update(obj);
        }

        public CrudResult<EntregaDoItemDaOrdemDeServico> Delete(int id)
        {
            return Engine.Entregas.Delete(Engine.Entregas.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
