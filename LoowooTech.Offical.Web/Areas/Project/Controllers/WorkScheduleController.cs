using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class WorkScheduleController : ProjectControllerBase
    {
        // GET: Project/WorkSchedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetialList(int projectId,int userId=0)
        {
            var list = Core.WorkScheduleManager.GetList(projectId, userId);
            ViewBag.List = list;
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Save(WorkSchedule workSchedule)
        {
            return SuccessJsonResult();
        }
    }
}