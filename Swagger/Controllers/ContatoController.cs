using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class ContatoController : ApiController
    {
        public CrudResult<Contato> Get() => Engine.Contatos.List();

        public CrudResult<Contato> Get(int id) => Engine.Contatos.Find(new object[] { id });

        public CrudResult<Contato> Post(Contato obj) => Engine.Contatos.Insert(obj);

        public CrudResult<Contato> Put(Contato obj) => Engine.Contatos.Update(obj);

        public CrudResult<Contato> Delete(int id) => Engine.Contatos.Delete(Engine.Contatos.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
