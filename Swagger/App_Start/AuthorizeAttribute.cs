using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Swagger.App_Start
{
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (!IsAuthorized(actionContext))
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}
