﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class ExpenseController : AdminControllerBase
    {
        // GET: Admin/Expense
        public ActionResult Index()
        {
            return View();
        }


    }
}