using LoowooTech.Models;
using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class TabletController : TabletsControllerBase
    {
        // GET: Tablets/Tablet
        public ActionResult Index(string serialNumber=null,int? TypeId=null,int? buyerId=null,int? accountId=null,int page=1,int rows=20)
        {
            var parameter = new TabletParameter
            {
                SerialNumer = serialNumber,
                TypeId = TypeId,
                AccountId=accountId,
                BuyerId=buyerId,
                Delete=false,
                Page = new PageParameter(page, rows)
            };
            var list = Core.TabletManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            ViewBag.Types = Core.TabletTypeManager.GetList();
            ViewBag.Accounts = Core.AccountManager.GetList();
            ViewBag.Users = Core.UserManager.GetList();
            return View();
        }

        [ChildActionOnly]
        public ActionResult Widget()
        {
            var parameter = new TabletParameter
            {
                Delete = false,
                Page = new PageParameter(1, 10)
            };
            var list = Core.TabletManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var model = Core.TabletManager.Get(id);
            ViewBag.Model = model;
            ViewBag.Types = Core.TabletTypeManager.GetList();
            ViewBag.Accounts = Core.AccountManager.GetList();
            ViewBag.Users = Core.UserManager.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Tablet tablet)
        {
            tablet.SerialNumber = tablet.SerialNumber.ToUpper();
            if (Core.TabletManager.Exist(tablet.SerialNumber, tablet.ID))
            {
                return ErrorJsonResult("当前系统中已存在相同的序列号！");
            }
            if (tablet.ID > 0)
            {
                if (!Core.TabletManager.Edit(tablet))
                {
                    return ErrorJsonResult("编辑平板信息失败！");
                }
            }
            else
            {
                
                var id = Core.TabletManager.Add(tablet);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加平板信息失败");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.TabletManager.Delete(id))
            {
                return ErrorJsonResult("删除平板失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Detail(int id)
        {
            var tablet = Core.TabletManager.Get(id);
            ViewBag.Model = tablet;
            return View();
        }
    }
}