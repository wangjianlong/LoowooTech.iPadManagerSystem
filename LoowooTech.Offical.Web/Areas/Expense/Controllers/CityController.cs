using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class CityController : ExpenseControllerBase
    {
        public ActionResult Select(int[] cityId = null)
        {
            var tree = Core.CityManager.GetTree();
            ViewBag.Tree = tree;
            ViewBag.Selected = cityId;
            return View();
        }
    }
}