using Generics.Extensoes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using Sec;
using Sec.Business;
using Sec.Business.Models;
using Sec.IdentityGroup;
using Sec.Models;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
            return Engine.Regras.Filter(p => p.Users.FirstOrDefault().UserId.Equals(id.ToString()));
        }

        [Route("~/api/Usuario/Login")]
        public string Login(Account obj)
        { 
            try
            {
                string url = string.Format("https://{0}{1}", Url.Request.RequestUri.Authority, "/token");
                Dictionary<string, string> p = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", obj.UserName },
                    { "password", obj.Senha }
                };
                FormUrlEncodedContent formUrlEncoded = new FormUrlEncodedContent(p);
                Uri uri = new Uri(url);
                HttpRequestMessage httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri, "token"),
                    Content = new StringContent("grant_type=password")
                };

                httpRequest.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
                httpRequest.Content = formUrlEncoded;

                using (var client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = client.SendAsync(httpRequest).Result;
                    string bearerData = httpResponse.Content.ReadAsStringAsync().Result;

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //am = new ApplicationManager();
                        //ApplicationUser au = am.UM.Find(obj.UserName.ToLower(), obj.Senha);

                        // usuario.AccessTokenFormat = JObject.Parse(bearerData)["access_token"].ToString();
                        // usuario.UserName = JObject.Parse(bearerData)["userName"].ToString();
                        return bearerData;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

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
