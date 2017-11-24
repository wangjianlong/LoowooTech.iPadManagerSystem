using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class TrafficController : AdminControllerBase
    {
        // GET: Admin/Traffic
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save()
        {
            return SuccessJsonResult();
        }
    }
}