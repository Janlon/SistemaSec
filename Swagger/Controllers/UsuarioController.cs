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

        public CrudResult<Usuario> Get()
        {
            return Engine.Usuarios.List();
        }

        public CrudResult<Usuario> Get(Guid id)
        {
            return Engine.Usuarios.Find(new object[] { id.ToString() });
        }

        /// <summary>
        /// Retorna a lista de permissoes de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Usuario/{id:Guid}/Permissoes")]
        public CrudResult<IdentityRole> GetPermissoesDoUsuario(Guid id)
        {
            CrudResult<IdentityRole> regras = new CrudResult<IdentityRole>();
            Usuario usuario = Engine.Usuarios.Filter(p => p.UserId.Equals(id.ToString())).Result.FirstOrDefault();
            Pessoa pessoa = Engine.Pessoas.Filter(p => p.Id.Equals(usuario.PessoaId)).Result.FirstOrDefault();
            am = new ApplicationManager();
            ApplicationUser user = am.UM.FindByEmail(pessoa.Email);   
            if(user != null)
            {
                 regras = Engine.Regras.Filter(p => p.Users.FirstOrDefault().UserId.Equals(user.Id));
            }
            return regras;
        }


        [Route("~/api/Login")]
        public CrudResult<IdentityRole> Login(Usuario obj)
        {
            CrudResult<IdentityRole> regras = new CrudResult<IdentityRole>();
            am = new ApplicationManager();
            ApplicationUser user = am.UM.FindByEmail(obj.UserName);

            Usuario usuario = Engine.Usuarios.Filter(p => p.UserId.Equals(user.Id.ToString())).Result.FirstOrDefault();
            Pessoa pessoa = Engine.Pessoas.Filter(p => p.Id.Equals(usuario.PessoaId)).Result.FirstOrDefault();
            if (user != null)
            {
                regras = Engine.Regras.Filter(p => p.Users.FirstOrDefault().UserId.Equals(user.Id));
            }
            return regras;
        }


        public CrudResult<Usuario> Post(Usuario obj)
        {
            Pessoa pessoa = Engine.Pessoas.Filter(p => p.Id.Equals(obj.PessoaId)).Result.FirstOrDefault();

            ApplicationManager am = new ApplicationManager();
            try
            { 
                List<IdentityResult> results = new List<IdentityResult>();
                try
                {
                    am = new ApplicationManager();
                    ApplicationUser usuario = am.UM.FindByEmail(pessoa.Email);
                    if (usuario == null)
                    {
                        results.Add(am.UM.Create(new ApplicationUser() { Email = pessoa.Email, UserName = pessoa.Email }, obj.Senha));
                       // am.Commit();
                        am.Dispose();
                        am = new ApplicationManager();
                        usuario = am.UM.FindByEmail(pessoa.Email);
                       // am.Commit();
                        am.Dispose();
                    }
                    if (usuario != null)
                        if (results != null)
                            if (results.Count() > 0)
                                if (results.Last().Succeeded)
                                {
                                    am = new ApplicationManager();
                                    am.UM.AddToRoles(usuario.Id, obj.Permissoes.Select(p => p.Name).ToArray());
                                   // am.Commit();
                                    am.Dispose();
                                }
                    obj.UserId = usuario.Id;
                }
                catch (Exception ex) { ex.Log(); }

            }
            catch (Exception ex) { ex.Log(); }
            finally { if (am != null) am.Dispose(); }

            return Engine.Usuarios.Insert(obj);
        }

        public CrudResult<Usuario> Put(Usuario obj)
        {
            return Engine.Usuarios.Update(obj);
        }

        public CrudResult<Usuario> Delete(Guid id)
        {
            return Engine.Usuarios.Delete(Engine.Usuarios.Filter(p => p.UserId.Equals(id.ToString())).Result.FirstOrDefault());
        }
    }
}
