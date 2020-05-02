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
        private ApplicationManager am;

        public CrudResult<Usuario> Get() => Engine.Usuarios.List();

        public CrudResult<Usuario> Get(int id) => Engine.Usuarios.Find(new object[] { id });


        /// <summary>
        /// Retorna a lista de permissoes de um usuário
        /// </summary>
        /// <param name="PessoaId"></param>
        /// <returns></returns>
        [Route("~/api/Usuario/{PessoaId:int}/Permissoes")]
        public CrudResult<IdentityRole> GetPermissoesDoUsuario(int PessoaId)
        {
            Usuario u = Engine.Usuarios.Filter(p => p.PessoaId.Equals(PessoaId)).Result.FirstOrDefault();
            am = new ApplicationManager();
            ApplicationUser usuario = am.UM.FindByEmail(u.User.Email);     
            var regras = Engine.Regras.Filter(p => p.Users.FirstOrDefault().UserId.Equals(usuario.Id));
            return regras;
        }

        public CrudResult<Usuario> Post(Usuario obj)
        {
            
            Pessoa pessoa = Engine.Pessoas.Filter(p=>p.Id.Equals(obj.PessoaId)).Result.FirstOrDefault();
            am = new ApplicationManager();
            ApplicationUser usuario = new ApplicationUser()
            {
                Email = pessoa.Email,
                UserName = pessoa.Email
            };
            obj.User = usuario;
            return Engine.Usuarios.Insert(obj);

            //List<IdentityRole> regras = new List<IdentityRole>();
            //foreach (RegraEnum temp in Enum.GetValues(typeof(RegraEnum)))
            //{
            //    regras.Add(new IdentityRole()
            //    {
            //        Name = temp.DisplayName()
            //    });
            //}

            //am.UM.AddToRoles(user.UserId, regras.Select(p => p.Name).ToArray()

            //var rg = Engine.Regras.Insert(am);


           // return Engine.Regras.Insert(am);
        }

        public CrudResult<Usuario> Put(Usuario obj) => Engine.Usuarios.Update(obj);

        public CrudResult<Usuario> Delete(int id) => Engine.Usuarios.Delete(Engine.Usuarios.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
