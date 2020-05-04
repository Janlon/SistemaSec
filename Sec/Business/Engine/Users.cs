namespace Sec.Business
{
    using Generics.Extensoes;
    using Microsoft.AspNet.Identity;
    using Sec.Business.Core;
    using Sec.Business.Models;
    using Sec.IdentityGroup;
    using Sec.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class Engine
    {

        [Obsolete("Todas as operações desta classe precisam ser revisadas, pois parte é inerente à Pessoas (entidade simples) e parte à ApplicationUser (entidade do tipo IdentityUser, vinculada à Owin e à Microsoft.Owin)")]
        public static class Users
        {

            [Obsolete()]
            public static async Task<CrudResult<RegisterViewModel>> Register(RegisterViewModel model)
            {
                CrudResult<RegisterViewModel> ret = new CrudResult<RegisterViewModel>(model);
                Pessoa pessoa = null;
                ApplicationUser u00 = null;
                // ->
                // Primeiro passo: Tentar criar a conta de acesso:
                try
                {
                    string senha = model.Password;
                    using (ApplicationManager am = new ApplicationManager())
                    {
                        u00 = await am.UM.FindAsync(model.Name, senha);
                        if (u00 != null)
                            ret.AddError("Usuário", "A conta não pode ser criada com os dados informados. Por favor, altere os dados e tente novamente.");
                        u00 = await am.UM.FindByEmailAsync(model.Email);
                        if (u00 != null)
                            ret.AddError("Usuário", "O email informado já está em uso. Por favor, altere os dados e tente novamente.");
                        if (ret.Success)
                        {
                            u00 = new ApplicationUser()
                            {
                                Email = model.Email,
                                UserName = model.Name
                            };
                            IdentityResult resp = await am.UM.CreateAsync(u00, model.Password);
                            if (resp.Succeeded == false)
                                foreach (string erro in resp.Errors)
                                    ret.AddError("Usuário", erro);
                            else
                            {
                                // ->
                                // Segundo passo: Criar a pessoa.
                                try
                                {
                                    if (u00 != null)
                                    {
                                        pessoa = new Pessoa()
                                        {
                                            //Apelido = model.NickName.Trim().ToUpper(),
                                            //Ativo = true,
                                            Cadastro = DateTime.Now,
                                            Email = model.Email.Trim().ToLower(),
                                            Nome = model.Name,
                                            CPF =model.CPF
                                        };
                                        using (Db db = new Db())
                                        {
                                            db.Pessoas.Add(pessoa);
                                            var vr = db.Entry(pessoa).GetValidationResult();
                                            if (!vr.IsValid)
                                                foreach (var ve in vr.ValidationErrors)
                                                    ret.AddError(ve.PropertyName, ve.ErrorMessage);
                                            else
                                            {
                                                ret.SetAffected(db.SaveChanges());
                                                // ->
                                                // Terceiro passo: Vincular pessoa e usuário:
                                                try
                                                {
                                                    Usuario u = new Usuario() { Pessoa = pessoa, User = u00 };
                                                    CrudResult<Usuario> tmp = new CrudResult<Usuario>(u);
                                                    using (UsuariosFactory uf = new UsuariosFactory())
                                                        ret.Include(uf.Create(u));
                                                }
                                                catch (Exception ex) { ret.AddError("Usuário", ex.Message); ex.Log(); }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex) { ret.AddError("Usuário", ex.Message); ex.Log(); }
                            }
                        }
                    }
                }
                catch (Exception ex) { ret.AddError("Usuário", ex.Message); ex.Log(); }
                return ret;
            }
        }
    }
}