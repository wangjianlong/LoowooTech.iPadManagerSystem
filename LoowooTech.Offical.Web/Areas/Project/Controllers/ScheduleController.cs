using LoowooTech.Models.Project;
using LoowooTech.Offical.Web.Common;
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

        public ActionResult Detail(int projectid)
        {
            var list = Identity.UserRole >= Models.UserRole.Advance ? Core.WorkScheduleManager.GetList(projectid, 0) : Core.WorkScheduleManager.GetList(projectid, Identity.UserId);
            ViewBag.List = list;
            ViewBag.ProjectId = projectid;
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
            var scheduleId = schedule.ID;
           
            if (schedule.ID > 0)
            {
                if (!Core.WorkScheduleManager.Edit(schedule))
                {
                    throw new ArgumentException("编辑工作记录失败");
                    //return ErrorJsonResult("编辑工作记录失败！");
                }
            }
            else
            {
                scheduleId = Core.WorkScheduleManager.Add(schedule);
                if (scheduleId <= 0)
                {
                    throw new ArgumentException("录入工作记录失败");
                    //return ErrorJsonResult("录入工作记录失败");
                }
            }
            #region 录入文件信息
            var fileIdList = new List<int>();
            if (Request.Files.Count > 0)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = HttpContext.Request.Files[i];
                    var fileName = file.FileName;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        continue;
                    }
                    var info = FilePost.Upload(file, "WorkSchedule", Identity.UserId);
                    if (info == null)
                    {
                        continue;
                    }
                    var fileId = Core.FileInfoManager.Add(info);
                    if (fileId > 0)
                    {
                        fileIdList.Add(fileId);
                    }
                }
            }
            if (fileIdList.Count > 0)
            {
                Core.WorkScheduleManager.AddFiles(fileIdList.Select(e => new WorkScheduleFiles { WorkScheduleId = scheduleId, FileId = e }).ToList());
            }
            #endregion
            return RedirectToAction("Detail", "Home", new { Id = schedule.ProjectId, activeLabel = "WorkSchedule" });
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