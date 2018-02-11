using LoowooTech.Models;
using LoowooTech.Models.Admin;
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

        public ActionResult Create(int id=0)
        {
            ViewBag.Sheet = Core.SheetManager.GetSingle(id);

            var companys = Core.UserCompanyManager.GetCompanys(Identity.UserId);
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
                    case SheetType.Reception:
                        return Redirect("/Expense/Reception/Create?sheetId=" + id);
                }
            }
            return View("Empty");
        }

        public ActionResult Examination(
            int? companyId=null,int? sheetUserId=null,int? flowNodeId=null,
            Models.Admin.VerificationState? state=null, int page=1,int rows=20)
        {


            var parameter = new SheetViewParameter
            {
                CheckUserId = Identity.UserId,
                CompanyId=companyId,
                SheetUserId=sheetUserId,
                FLowNodeId=flowNodeId,
                State = state,
                Page = new PageParameter(page, rows)
            };
            var sheets = Core.SheetViewManager.Search(parameter);
            ViewBag.Sheets = sheets;
            ViewBag.Parameter = parameter;
            var companys = Core.CompanyManager.GetList();
            ViewBag.Companys = companys;
            var users = Core.UserManager.GetList();
            ViewBag.Users = users;
            ViewBag.FlowNodes = Core.FlowNodeManager.GetList2(Flow.ID);
            return View();
        }

        /// <summary>
        /// 报销确认款项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Check(int id,bool flag)
        {
            if (!Core.SheetManager.IsCheck(id, flag))
            {
                return ErrorJsonResult("确认款项失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Head(SheetType type)
        {
            var parameter = new SheetViewParameter
            {
                SheetUserId = Identity.UserId,
                SheetType=type,
                Page = new PageParameter(1, 20)
            };
            var list = Core.SheetViewManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult State(int userId,FlowDataState state,bool? isCheck, int? page = null,int rows=20)
        {
            var parameter = new SheetFlowDataParameter
            {
                UserId = userId,
                State = state,
                IsCheck = isCheck
            };
            if (page.HasValue)
            {
                parameter.Page = new PageParameter(page.Value, rows);
            }
            var list = Core.SheetFlowDataViewManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }

    }
}