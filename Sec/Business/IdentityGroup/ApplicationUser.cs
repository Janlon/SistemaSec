namespace Sec.IdentityGroup
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager, string authenticationType)
        {
            throw new NotImplementedException();
        }
    }
}