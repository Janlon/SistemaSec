namespace Sec.IdentityGroup
{
    using Generics.Extensoes;
    using Sec.Business;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                ApplicationManager am = new ApplicationManager();
                ApplicationUser user = await am.UM.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Nome de usuário ou senha incorreto.");
                    return;
                }
                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(am, OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(am, CookieAuthenticationDefaults.AuthenticationType);
                string dll = "";
                try 
                {
                    Assembly currentAssem = Assembly.GetExecutingAssembly();
                    dll = currentAssem.FullName;
                } catch { dll = "Sec"; }
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Actor, dll));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.IsPersistent, "true"));
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.Now.Add(Startup.ExpireTimespan).ToUniversalTime().ToString()));
                try
                {
                    RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new Db()));
                    foreach (ApplicationUserRole regra in user.Roles)
                    {
                        string strRegra = rm.FindById(regra.RoleId).Name;
                        oAuthIdentity.AddClaim(new Claim("role", strRegra));
                        cookiesIdentity.AddClaim(new Claim("role", strRegra));
                    }
                }
                catch (Exception ex) { ex.Log(); }
                finally { if (am != null) am.Dispose(); }
                AuthenticationProperties properties = CreateProperties(user.UserName);
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            catch (Exception ex) { ex.Log(); }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // As credenciais de senha do proprietário do recurso não fornecem um ID de cliente.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}
