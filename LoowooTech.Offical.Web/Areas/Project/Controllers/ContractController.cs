using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class ContractController : ProjectControllerBase
    {
        // GET: Project/Contract
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int projectId)
        {
            var companys = Core.CompanyManager.GetAll();
            ViewBag.Companys = companys;
            return View();
        }

        public ActionResult Detail(int projectId)
        {

            return View();
        }
    }
}