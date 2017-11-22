using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class HomeController : ExpenseControllerBase
    {
        // GET: Expense/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var companys = Core.CompanyManager.GetList();
            ViewBag.Companys = companys;

            return View();
        }

        [HttpPost]
        public ActionResult Save()
        {
            return SuccessJsonResult();
        }


    }
}