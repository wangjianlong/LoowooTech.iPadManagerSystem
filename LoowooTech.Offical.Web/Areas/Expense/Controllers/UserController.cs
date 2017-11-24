using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class UserController : ExpenseControllerBase
    {
        public ActionResult Select(int[] UserId=null)
        {
            var groups = Core.GroupManager.GetList();
            ViewBag.Groups = groups;
            ViewBag.Selected = UserId;
            return View();
        }
    }
}