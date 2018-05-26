using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Project.Controllers
{
    public class HomeController : ProjectControllerBase
    {
        // GET: Project/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            ViewBag.Companys = Core.CompanyManager.GetList();
            ViewBag.Projects = Core.CompanyManager.GetProjects();
            ViewBag.Citys = Core.CityManager.GetTree();
            ViewBag.ProjectTypes = Core.ProjectTypeManager.GetTree();
            ViewBag.Users = Core.UserManager.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Models.Project.Project project,int[] userId)
        {
            if (string.IsNullOrEmpty(project.Name))
            {
                return ErrorJsonResult("请填写项目名称");
            }
            if (project.ID > 0)
            {
                if (!Core.ProjectManager.Edit(project))
                {
                    return ErrorJsonResult("编辑项目信息失败！");
                }
            }
            else
            {
                var id = Core.ProjectManager.Add(project);
            }

            if (userId != null && userId.Count() > 0)
            {
                Core.ProjectManager.SaveProjectUser(userId.Select(e => new ProjectUser { ProjectId = project.ID, UserId = e, Relation = ProjectRelation.Participation }).ToList(), project.ID);
            }
            return SuccessJsonResult();
        }

        public ActionResult Recent(int? userId = null)
        {
            var parameter = new ProjectParameter
            {
                UserId = userId,
                Order=Models.LWOrder.DeTime,
                Page = new Models.PageParameter(1, 20)
            };
            var list = Core.ProjectManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }
        public ActionResult Detail(int id)
        {
            var project = Core.ProjectManager.Get(id);
            ViewBag.Project = project;
            return View();
        }
    }
}