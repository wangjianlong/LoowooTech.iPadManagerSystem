﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class HomeController : TabletsControllerBase
    {
        // GET: Tablets/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}