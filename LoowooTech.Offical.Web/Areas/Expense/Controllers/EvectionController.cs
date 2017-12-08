using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Expense.Controllers
{
    public class EvectionController : ExpenseControllerBase
    {
        // GET: Expense/Evection
        public ActionResult Index()
        {
            var parameter = new SheetParameter
            {
                UserId = Identity.UserId,
                Type = SheetType.Evection,
                Page = new Models.PageParameter(1, 20)
            };
            var list = Core.SheetManager.Search(parameter);
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int SheetId)
        {
            var sheet = Core.SheetManager.Get(SheetId);
            ViewBag.Sheet = sheet;
            ViewBag.Citys = Core.CityManager.GetTree();
            return View();
        }
        [HttpPost]
        public ActionResult Save(Evection evection,int[] userId,DateTime[] startTime,DateTime[] endTime,Vehicles[] vehicles,double[] cost,double[] kiloMeter,double[] carPetty,string[] plate,string[] driver,double[] petrol,double[] toll)
        {
            if (evection.ID > 0)
            {
                if (!Core.EvectionManager.Edit(evection))
                {
                    return ErrorJsonResult("编辑出差基础信息失败！");
                }
            }
            else
            {
                var id = Core.EvectionManager.Add(evection);
                if (id <= 0)
                {
                    return ErrorJsonResult("录入出差基础信息失败！");
                }
            }
            var traffics = Traffic.Generate(evection.ID, vehicles, cost, kiloMeter, carPetty, plate, driver, petrol, toll);
            var errands = Errand.Generate(evection.ID, userId, startTime, endTime);
            var message = string.Empty;
            if (traffics != null && traffics.Count > 0)
            {
                Core.TrafficManager.Save(evection.ID, traffics);
            }
            else
            {
                message = "未获取任何交通费相关信息，请核对!";
            }
            if (errands != null && errands.Count > 0)
            {
                Core.ErrandManager.Save(evection.ID, errands);
            }
            else
            {
                message += "未获取出差人员相关时间信息，请核对!";
            }
            if (!string.IsNullOrEmpty(message))
            {
                return ErrorJsonResult(message);
            }

            return SuccessJsonResult();
        }


        public ActionResult Detail(int sheetId)
        {
            var sheet = Core.SheetManager.Get(sheetId);
            ViewBag.Sheet = sheet;
            return View();
        }

    }
}