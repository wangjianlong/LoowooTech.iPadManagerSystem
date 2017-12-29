using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class DailyController : ExpenseControllerBase
    {
        // GET: Expense/Daily
        public ActionResult Index()
        {
            var parameter = new SheetParameter
            {
                UserId = Identity.UserId,
                Type = SheetType.Daily,
                Page = new Models.PageParameter(1, 20)
            };
            var list = Core.SheetManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }

        public ActionResult Search(int? CategoryId=null,int? CompanyId=null,DateTime? startTime=null,DateTime? endTime=null,string content=null, int page=1,int rows=20)
        {
            //var parameter=new DailyParameter
            //{

            //}
            return View();
        }

        public ActionResult Create(int sheetId)
        {
            var sheet = Core.SheetManager.Get(sheetId,Flow.ID);
            ViewBag.Sheet = sheet;
            ViewBag.Category = Core.CategoryManager.GetTree();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Daily Daily, int[] CategoryId,double[] Cost,string[] content)
        {
            if (Daily.ID > 0)
            {
                if (!Core.DailyManager.Edit(Daily))
                {
                    return ErrorJsonResult("编辑修改日常信息失败！");
                }
            }
            else
            {
                var id = Core.DailyManager.Add(Daily);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加日常基础信息失败！");
                }
            }
            var substances = Substance.Generate(Daily.ID, CategoryId, Cost, content);
            if (substances != null && substances.Count > 0)
            {
                Core.SubstanceManager.Save(Daily.ID, substances);
            }
            else
            {
                return ErrorJsonResult("保存日常项目失败！");
            }
            if (!Core.SheetManager.UpdateMoney(Daily.SheetId, substances.Sum(e => e.Cost)))
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
    }
}