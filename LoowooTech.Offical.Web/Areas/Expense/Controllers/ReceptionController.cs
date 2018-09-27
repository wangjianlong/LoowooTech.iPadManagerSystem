using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class ReceptionController : ExpenseControllerBase
    {
        // GET: Expense/Reception
        public ActionResult Index()
        {
            var parameter = new SheetParameter
            {
                UserId = Identity.UserId,
                Type = SheetType.Reception,
                Delete=false,
                Page = new LoowooTech.Models.PageParameter(1, 20)
            };
            var list = Core.SheetManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }
        public ActionResult Create(int sheetId)
        {
            var sheet = Core.SheetManager.Get(sheetId,Flow.ID);
            ViewBag.Sheet = sheet;
            var list = Core.CompanyManager.GetProjects();
            ViewBag.Projects = list;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Reception reception,string[] content,double[] cost,PayWay[] way,string[] memo)
        {
            if (reception.ID > 0)
            {
                if (!Core.ReceptionManager.Edit(reception))
                {
                    return ErrorJsonResult("编辑招待基础信息失败！");
                }
            }
            else
            {
                var id = Core.ReceptionManager.Add(reception);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加招待基础信息失败！");
                }
            }
            var items = ReceptionItem.Generate(reception.ID, content, cost, way, memo);
            if (items != null && items.Count > 0)
            {
                Core.ReceptionItemManager.Save(reception.ID, items);
            }
            else
            {
                return ErrorJsonResult("招待分项内容获取失败！请刷新重试");
            }
            reception.Items = items;
            if (!Core.SheetManager.UpdateMoney(reception.SheetId, reception.Sum))
            {
                return ErrorJsonResult("更新报销金额失败！");
            }
            return SuccessJsonResult();
        }
       

        public ActionResult Detail(int sheetId)
        {
            var sheet = Core.SheetManager.Get(sheetId,Flow.ID);
            ViewBag.Sheet = sheet;
            return View();
        }

        public ActionResult Search(int? companyId=null,int?projectId=null,
            DateTime? startTime=null,DateTime? endTime=null,bool? Delete=null,
            int? companyId2=null,int? userId2=null,PayWay? way=null,string itemContent=null,
            string content=null,int page=1,int rows=20)
        {
            var parameter = new ReceptionParameter
            {
                CompanyId = companyId,
                ProjectId = projectId,
                StartTime = startTime,
                EndTime = endTime,
                UserId = Identity.UserId,
                CompanyId2 = companyId2,
                UserId2 = userId2,
                Way = way,
                ItemContent = itemContent,
                Content = content,
                Delete=Delete,
                Page = new Models.PageParameter(page, rows)
            };
            ViewBag.Companys = Core.UserCompanyManager.GetCompanys(Identity.UserId);
            ViewBag.Projects = Core.ProjectManager.Search(new Models.Project.ProjectParameter { Delete = false });
            ViewBag.Companys2 = Core.CompanyManager.GetProjects();
            var list = Core.ReceptionManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }
    }
}