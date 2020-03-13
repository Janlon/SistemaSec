using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;

[assembly: OwinStartup(typeof(Sec.IdentityGroup.Startup))]
namespace Sec.IdentityGroup
{
    using System.Web;
    using System.Web.Mvc;

    public class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new Microsoft.Owin.Security.AuthenticationProperties
            {
                RedirectUri = RedirectUri
            };
            context.HttpContext.GetOwinContext()
                .Authentication
                .Challenge(properties, LoginProvider);
        }
    }
}
