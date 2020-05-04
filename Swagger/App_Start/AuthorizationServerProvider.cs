using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Swagger.App_Start
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (context.Password.Equals("Senha@123") && context.UserName.Equals("olga@email.com"))
                context.Validated(identity);
            else
            {
                context.SetError("Usuário e senha inválidos");
                return;
            }
        }
    }
}