using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class ProjectTypeController : AdminControllerBase
    {
        // GET: Admin/ProjetType
        public ActionResult Index()
        {
            var tree = Core.ProjectTypeManager.GetTree();
            ViewBag.Tree = tree;
            return View();
        }

        public ActionResult Create(int id = 0,int parentId=0)
        {
            var projectType = Core.ProjectTypeManager.Get(id);
            ViewBag.ProjectType = projectType;
            ViewBag.Parent = Core.ProjectTypeManager.Get(parentId);
            return View();
        }

        [HttpPost]
        public ActionResult Save(ProjectType projectType)
        {
            if (projectType.ID > 0)
            {
                if (!Core.ProjectTypeManager.Edit(projectType))
                {
                    return ErrorJsonResult("编辑项目类型失败!");
                }
            }
            else
            {
                var id = Core.ProjectTypeManager.Add(projectType);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加项目类型失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.ProjectTypeManager.Delete(id))
            {
                return ErrorJsonResult("删除项目类型，未找到相关项目类型信息！");
            }
            return SuccessJsonResult();
        }
    }
}