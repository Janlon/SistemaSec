using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Swagger.App_Start.Startup))]

namespace Swagger.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var Provedor = new AuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(double.Parse(ConfigurationManager.AppSettings["TempoDeVidaDoTokenEmHoras"])),
                Provider = Provedor
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            SwaggerConfig.Register(config: config);
        }
    }
}
