using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class ScheduleController : ProjectControllerBase
    {
        // GET: Project/Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id = 0,int projectId=0)
        {
            var model = Core.WorkScheduleManager.Get(id);
            ViewBag.Model = model;
            var project = Core.ProjectManager.Get(projectId);
            ViewBag.Project = project;
            return View();
        }

        [HttpPost]
        public ActionResult Save(WorkSchedule schedule)
        {
            if (schedule.ID > 0)
            {
                if (!Core.WorkScheduleManager.Edit(schedule))
                {
                    return ErrorJsonResult("编辑工作记录失败！");
                }
            }
            else
            {
                var id = Core.WorkScheduleManager.Add(schedule);
                if (id <= 0)
                {
                    return ErrorJsonResult("录入工作记录失败");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.WorkScheduleManager.Delete(id, true))
            {
                return ErrorJsonResult("删除工作记录失败！");
            }
            return SuccessJsonResult();
        }
    }
}