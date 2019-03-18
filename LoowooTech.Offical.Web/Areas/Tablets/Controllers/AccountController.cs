using LoowooTech.Models;
using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Tablets.Controllers
{
    public class AccountController : TabletsControllerBase
    {
        // GET: Tablets/Account
        public ActionResult Index()
        {
            var list = Core.AccountManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var model = Core.AccountManager.Get(id);
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Account account)
        {
            if (account.ID > 0)
            {
                if (!Core.AccountManager.Edit(account))
                {
                    return ErrorJsonResult("编辑账号失败！");
                }
            }
            else
            {
                var id = Core.AccountManager.Add(account);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加账号失败！");
                }
            }

            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.AccountManager.Delete(id))
            {
                return ErrorJsonResult("删除账号失败！");
            }
            return SuccessJsonResult();
        }

        public ActionResult Detail(int id)
        {
            var account = Core.AccountManager.Get(id);
            ViewBag.Model = account;
            return View();
        }


    }
}