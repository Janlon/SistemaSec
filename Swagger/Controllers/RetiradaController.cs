using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class RetiradaController : ApiController
    {
        public CrudResult<RetiradaDoItemDaOrdemDeServico> Get()
        {
            return Engine.Retiradas.List();
        }

        public CrudResult<RetiradaDoItemDaOrdemDeServico> Get(int id)
        {
            return Engine.Retiradas.Find(new object[] { id });
        }

        [Route("~/api/Retirada/{ItemId:int}/Item")]
        public CrudResult<RetiradaDoItemDaOrdemDeServico> GetItemRetirado(int ItemId)
        {
            return Engine.Retiradas.Filter(p => p.ItemDaOrdemDeServicoId.Equals(ItemId));
        }


        public CrudResult<RetiradaDoItemDaOrdemDeServico> Post(RetiradaDoItemDaOrdemDeServico obj)
        {
            return Engine.Retiradas.Insert(obj);
        }

        public CrudResult<RetiradaDoItemDaOrdemDeServico> Put(RetiradaDoItemDaOrdemDeServico obj)
        {
            return Engine.Retiradas.Update(obj);
        }

        public CrudResult<RetiradaDoItemDaOrdemDeServico> Delete(int id)
        {
            return Engine.Retiradas.Delete(Engine.Retiradas.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
