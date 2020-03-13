using Sec.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class MailController : ApiController
    {
        public bool Send([FromBody] string body, string subject, string fromAddress, string fromName, string toAddress, string toName)
        {
            return Engine.SendEmail(body, subject, fromAddress, fromName, toAddress, toName);
        }

    }
}
