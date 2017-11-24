using LoowooTech.Models.Expense;
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
            ViewBag.Projects = Core.ProjectManager.Search(new Models.Project.ProjectParameter { IsDone = false, Delete = false });
            return View();
        }

        [HttpPost]
        public ActionResult Save(Sheet sheet,string projectName)
        {
            if (sheet.UserId == 0||sheet.CompanyId==0)
            {
                return ErrorJsonResult("未获取报销人信息或者报销单位信息");
            }
            if (sheet.ProjectId == 0)
            {
                if (string.IsNullOrEmpty(projectName))
                {
                    return ErrorJsonResult("请选择相关项目");
                }
                else
                {
                    if (Core.ProjectManager.Exist(projectName))
                    {
                        return ErrorJsonResult("当前输入项目名称已经存在，请核对！");
                    }
                    var projectId = Core.ProjectManager.Add(new Models.Project.Project { Name = projectName, UserId = sheet.UserId });
                    if (projectId <= 0)
                    {
                        return ErrorJsonResult("录入手工输入项目失败！");
                    }
                    else
                    {
                        sheet.ProjectId = projectId;
                    }
                }
            }
            if (sheet.ID > 0)
            {
                if (!Core.SheetManager.Edit(sheet))
                {
                    return ErrorJsonResult("编辑报销基础信息失败！");
                }
            }
            else
            {
                var id = Core.SheetManager.Add(sheet);
                if (id <= 0)
                {
                    return ErrorJsonResult("创建报销基础信息失败");
                }
            }
            return SuccessJsonResult(sheet.ID);
        }

        public ActionResult Delete(int id)
        {
            if (!Core.SheetManager.Delete(id))
            {
                return ErrorJsonResult("删除失败！未找到相关基础报销信息！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Navigation(int id)
        {
            var sheet = Core.SheetManager.Get(id);
            if (sheet != null)
            {
                switch (sheet.SheetType)
                {
                    case SheetType.Evection:
                        return Redirect("/Expense/Evection/Create?sheetId=" + id);
                    case SheetType.Daily:
                        return Redirect("/Expense/Daily/Create?sheetId=" + id);
                }
            }
            return View("Empty");
        }

    }
}