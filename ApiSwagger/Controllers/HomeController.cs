using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace ApiSwagger.Controllers
{
    public class HomeController : Controller
    {

        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            return Redirect("/swagger/");
        }

    }
}
