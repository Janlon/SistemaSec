using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class OrdemServicoController : ApiController
    {

        public CrudResult<OrdemDeServico> Get()
        {
            return Engine.OrdensDeServicos.List();
        }

        public CrudResult<OrdemDeServico> Get(int id)
        {
            return Engine.OrdensDeServicos.Find(new object[] { id });
        }

        /// <summary>
        /// Retorna a lista de itens de uma ordem de serviço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/OrdemServico/{id:int}/itens")]
        public CrudResult<OrdemDeServico> GetItensDaOrdemServico(int id)
        {
            return Engine.OrdensDeServicos.Filter(p => p.Itens.Equals(id));
        }

        public CrudResult<OrdemDeServico> Post(OrdemDeServico obj)
        {
            return Engine.OrdensDeServicos.Insert(obj);
        }

        public CrudResult<OrdemDeServico> Put(OrdemDeServico obj)
        {
            return Engine.OrdensDeServicos.Update(obj);
        }

        public CrudResult<OrdemDeServico> Delete(int id)
        {
            return Engine.OrdensDeServicos.Delete(Engine.OrdensDeServicos.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
