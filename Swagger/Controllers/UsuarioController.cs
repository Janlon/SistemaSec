using Generics.Extensoes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sec;
using Sec.Business;
using Sec.IdentityGroup;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using ApplicationManager = Sec.IdentityGroup.ApplicationManager;

namespace Swagger.Controllers
{
    public class UsuarioController : ApiController
    {
        public CrudResult<Usuario> Get() => Engine.Usuarios.List();

        public CrudResult<Usuario> Get(int id) => Engine.Usuarios.Find(new object[] { id });


        /// <summary>
        /// Retorna a lista de permissoes de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Usuario/{Id:int}/Permissoes")]
        public CrudResult<IdentityRole> GetPermissoesDoUsuario(int id)
        {
            var usuarios = Engine.Usuarios.Filter(p => p.PessoaId.Equals(id)).Result;
            return null;
        }

        public CrudResult<Usuario> Post(Usuario obj)
        {
            //List<IdentityResult> results = new List<IdentityResult>();
            //try
            //{
            //    List<IdentityRole> regras = new List<IdentityRole>();

            //    foreach (RegraEnum temp in Enum.GetValues(typeof(RegraEnum)))
            //    {
            //        regras.Add(new IdentityRole()
            //        {
            //            Name = temp.DisplayName()
            //        });
            //    }

            //    am = new ApplicationManager();

            //    ApplicationUser usuario = am.UM.FindByEmail(email);

            //    if (usuario == null)
            //    {
            //        results.Add(am.UM.Create(new ApplicationUser() { Email = email, UserName = email }, senha));
            //        am.Commit();
            //        am.Dispose();
            //        am = new ApplicationManager();
            //        usuario = am.UM.FindByEmail(email);
            //        am.Commit();
            //        am.Dispose();
            //    }
            //    if (usuario != null)
            //        if (results != null)
            //            if (results.Count() > 0)
            //                if (results.Last().Succeeded)
            //                {
            //                    am = new ApplicationManager();
            //                    am.UM.AddToRoles(usuario.Id, regras.Select(p => p.Name).ToArray());
            //                    am.Commit();
            //                    am.Dispose();
            //                }
            //}
            //catch (Exception ex) { ex.Log(); }
            //return results;
            return Engine.Usuarios.Insert(obj);
        }

        public CrudResult<Usuario> Put(Usuario obj) => Engine.Usuarios.Update(obj);

        public CrudResult<Usuario> Delete(int id) => Engine.Usuarios.Delete(Engine.Usuarios.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
