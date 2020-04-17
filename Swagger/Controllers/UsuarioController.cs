using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class UsuarioController : ApiController
    {
        public CrudResult<Usuario> Get() => Engine.Usuarios.List();

        public CrudResult<Usuario> Get(int id) => Engine.Usuarios.Find(new object[] { id });

        public CrudResult<Usuario> Post(Usuario obj) => Engine.Usuarios.Insert(obj);

        public CrudResult<Usuario> Put(Usuario obj) => Engine.Usuarios.Update(obj);

        public CrudResult<Usuario> Delete(int id) => Engine.Usuarios.Delete(Engine.Usuarios.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
