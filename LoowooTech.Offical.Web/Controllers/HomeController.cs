using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            if (!Identity.IsAuthenticated)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
    }
}