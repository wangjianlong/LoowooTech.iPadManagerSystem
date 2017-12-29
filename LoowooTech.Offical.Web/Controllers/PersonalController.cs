using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Controllers
{
    public class PersonalController : ControllerBase
    {
        // GET: Personal
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Edit(int id)
        {
            var user = Core.UserManager.Get(id);
            ViewBag.User = user;
            return View();
        }
    }
}