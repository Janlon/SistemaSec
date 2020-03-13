namespace Sec.IdentityGroup
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    internal static class ApplicationUserExtensions
    {
        /// <summary>
        /// Sobreposição do método.
        /// </summary>
        /// <param name="authenticartionType">Tipo de autenticação desejado.</param>
        /// <returns></returns>
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this ApplicationUser user, string authenticartionType)
        {
            ClaimsIdentity ret = null;
            using (ApplicationManager am = new ApplicationManager())
                ret = await am.UM.CreateIdentityAsync(user, authenticartionType);
            return ret;
        }
        /// <summary>
        /// Sobreposição do método.
        /// </summary>
        /// <param name="authenticartionType">Tipo de autenticação desejado.</param>
        /// <returns></returns>
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this ApplicationUser user, ApplicationManager am, string authenticartionType)
        {
            ClaimsIdentity ret = await am.UM.CreateIdentityAsync(user, authenticartionType);
            return ret;
        }
    }    
}