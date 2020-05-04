using Newtonsoft.Json;
using SiteSec.Models;
using SiteSec.Models.Consumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly Api api = new Api();

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Account obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            bool isValido = await api.Token(obj);
            if (isValido)
                return Redirect("~/Home/index");

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Index", "Account");
        }

    }
}