namespace Sec.IdentityGroup
{
    using Generics.Extensoes;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationUser : IdentityUser
    {
        private ApplicationManager am;

        public List<IdentityResult> GenerateUserIdentityAsync(ApplicationUserManager userManager)
        {
            List<IdentityResult> results = new List<IdentityResult>();

            string email = userManager.Users.First().Email;
            string senha = userManager.Users.First().PasswordHash;
         
            try
            {
                List<IdentityRole> regras = new List<IdentityRole>();

                foreach (RegraEnum temp in Enum.GetValues(typeof(RegraEnum)))
                {
                    regras.Add(new IdentityRole() 
                    { 
                        Name = temp.DisplayName() 
                    });
                }

                am = new ApplicationManager();

                ApplicationUser usuario = am.UM.FindByEmail(email);

                if (usuario == null)
                {
                    results.Add(am.UM.Create(new ApplicationUser() { Email = email, UserName = email }, senha));
                    am.Commit();
                    am.Dispose();
                    am = new ApplicationManager();
                    usuario = am.UM.FindByEmail(email);
                    am.Commit();
                    am.Dispose();
                }
                if (usuario != null)
                    if (results != null)
                        if (results.Count() > 0)
                            if (results.Last().Succeeded)
                            {
                                am = new ApplicationManager();
                                am.UM.AddToRoles(usuario.Id, regras.Select(p => p.Name).ToArray());
                                am.Commit();
                                am.Dispose();
                            }
            }
            catch (Exception ex) { ex.Log(); }
            return results;
        }
    }
}