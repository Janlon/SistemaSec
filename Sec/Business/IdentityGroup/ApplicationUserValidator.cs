namespace Sec.IdentityGroup
{
    using Microsoft.AspNet.Identity;


    public class ApplicationUserValidator: UserValidator<ApplicationUser>
    {
        public ApplicationUserValidator(UserManager<ApplicationUser> context) : base(context)
        {
            AllowOnlyAlphanumericUserNames = false;
            RequireUniqueEmail = true;
        }
    }
}