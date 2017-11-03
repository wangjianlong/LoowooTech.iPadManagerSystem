using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Controllers
{
    public class LWSystemController : ControllerBase
    {
        // GET: LWSystem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            return View();
        }
    }
}