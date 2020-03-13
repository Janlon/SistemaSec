namespace Sec.IdentityGroup
{
    using Microsoft.AspNet.Identity;
    
    public class ApplicationPasswordValidator : PasswordValidator
    {
        public ApplicationPasswordValidator()
        {
            RequiredLength = 6;
            RequireNonLetterOrDigit = true;
            RequireDigit = true;
            RequireLowercase = true;
            RequireUppercase = true;
        }
    }
}