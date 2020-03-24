using Microsoft.Owin;

[assembly: OwinStartup(typeof(Sec.IdentityGroup.Startup))]
namespace Sec.IdentityGroup
{
    using Generics.Extensoes;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security.OAuth;
    using Owin;

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public partial class Startup
    {
        private static int expireHours { get; set; }

        internal static TimeSpan ExpireTimespan
        {
            get
            {
                int timeOut = 24;
                if (expireHours <= 0)
                    if (ConfigurationManager.AppSettings["SectionExpirationHours"] != null)
                    {
                        if (!(int.TryParse(ConfigurationManager.AppSettings["SectionExpirationHours"], out timeOut)))
                            timeOut = 24; expireHours = timeOut;
                    }
                    else
                        expireHours = timeOut;
                return TimeSpan.FromHours(expireHours);
            }
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
            DataInit();
        }

        public void Configuration(IAppBuilder app, string tokenPath)
        {
            ConfigureAuth(app, tokenPath);
            DataInit();
        }

        public void Configuration(IAppBuilder app, string tokenPath, string loginPath)
        {
            ConfigureAuth(app, tokenPath, loginPath);
            DataInit();
        }

        private void DataInit()
        {
            ApplicationManager am = new ApplicationManager();
            try
            {
                List<IdentityRole> regras = new List<IdentityRole>();
                foreach (RegraEnum temp in Enum.GetValues( typeof(RegraEnum)))
                    regras.Add(new IdentityRole() { Name = temp.DisplayName() });
                List<IdentityResult> results = new List<IdentityResult>();
                try
                {
                    if (am.RM.Roles.Count() == 0)
                        foreach (var regra in regras)
                            if (!am.RM.RoleExists(regra.Name))
                                results.Add(am.RM.Create(regra));
                    am.Commit();
                    am.Dispose();
                }
                catch (Exception ex) { ex.Log(); }

                try 
                {
                    am = new ApplicationManager();
                    regras = am.RM.Roles.ToList();
                    am.Commit();
                    am.Dispose();
                } catch (Exception ex) { ex.Log(); }

                try
                {
                    am = new ApplicationManager();
                    ApplicationUser usuario = am.UM.FindByEmail("iosystems@hotmail.com");
                    if (usuario == null)
                    {
                        results.Add(am.UM.Create(new ApplicationUser() { Email = "iosystems@hotmail.com", UserName = "iosystems@hotmail.com" }, "Senha@123"));
                        am.Commit();
                        am.Dispose();
                        am = new ApplicationManager();
                        usuario = am.UM.FindByEmail("iosystems@hotmail.com");
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

            }
            catch (Exception ex) { ex.Log(); }
            finally { if (am != null) am.Dispose(); }
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath =
                new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = ExpireTimespan,
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public void ConfigureAuth(IAppBuilder app, string tokenPath = "/Token")
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(tokenPath),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath =
                new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = ExpireTimespan,
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
        public void ConfigureAuth(IAppBuilder app, string tokenPath = "/Token", string loginPath = "/api/Account/ExternalLogin")
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(tokenPath),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath =
                new PathString(loginPath),
                AccessTokenExpireTimeSpan = ExpireTimespan,
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }        
    }
}
