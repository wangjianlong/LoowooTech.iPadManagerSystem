using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Version.Controllers
{
    public class HomeController : VersionsControllerBase
    {
        // GET: Version/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}