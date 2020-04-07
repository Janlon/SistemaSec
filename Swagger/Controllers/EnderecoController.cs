using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class EnderecoController : ApiController
    {

        public CrudResult<Endereco> Get()
        {
            return Engine.Enderecos.List();
        }

        public CrudResult<Endereco> Get(int id)
        {
            return Engine.Enderecos.Find(new object[] { id });
        }

        public CrudResult<Endereco> Post(Endereco obj)
        {
            return Engine.Enderecos.Insert(obj);
        }

        public CrudResult<Endereco> Put(Endereco obj)
        {
            return Engine.Enderecos.Update(obj);
        }

        public CrudResult<Endereco> Delete(int id)
        {
            return Engine.Enderecos.Delete(Engine.Enderecos.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
