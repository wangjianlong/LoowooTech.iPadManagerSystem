﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class DailyController : ExpenseControllerBase
    {
        // GET: Expense/Daily
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int sheetId)
        {

            return View();
        }
    }
}