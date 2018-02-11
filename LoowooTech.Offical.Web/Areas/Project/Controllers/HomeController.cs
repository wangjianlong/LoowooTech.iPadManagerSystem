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
            return View();
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