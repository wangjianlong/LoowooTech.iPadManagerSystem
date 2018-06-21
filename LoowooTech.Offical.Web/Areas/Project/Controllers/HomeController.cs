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
        public ActionResult Save(Models.Project.Project project,int[] usersId)
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

            if (usersId != null && usersId.Count() > 0)
            {
                Core.ProjectManager.SaveProjectUser(usersId.Select(e => new ProjectUser { ProjectId = project.ID, UserId = e, Relation = ProjectRelation.Participation }).ToList(), project.ID);
            }
            return SuccessJsonResult(project.ID);
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
        public ActionResult Detail(int id,string activeLabel=null)
        {
            var project = Core.ProjectManager.Get(id);
            ViewBag.Project = project;
            ViewBag.ProjectUsers = Core.ProjectManager.GetUsers(id);
            ViewBag.ActiveLabel = activeLabel;
            return View();
        }


        public ActionResult Search(int? year=null,int? companyaId=null,int? companybId=null,int? projectTypeId=null,int? cityId=null, string name=null,int? userId=null, int page=1,int rows=20)
        {
            var parameter = new ProjectParameter
            {
                Year=year,
                CompanyAId=companyaId,
                CompanyBId=companybId,
                ProjectTypeId=projectTypeId,
                CityId=cityId,
                Name = name,
                UserId = userId,
                Page = new Models.PageParameter(page, rows)
            };
            var list = Core.ProjectManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;

            ViewBag.Companys = Core.CompanyManager.GetList();
            ViewBag.Projects = Core.CompanyManager.GetProjects();
            ViewBag.Citys = Core.CityManager.GetTree();
            ViewBag.ProjectTypes = Core.ProjectTypeManager.GetTree();
            ViewBag.Users = Core.UserManager.GetList();
            return View();

        }
    }
}