using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Swagger
{
    public class HubSignalIr : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}