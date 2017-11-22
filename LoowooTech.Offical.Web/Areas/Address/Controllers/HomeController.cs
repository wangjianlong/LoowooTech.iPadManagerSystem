using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Address.Controllers
{
    public class HomeController : AddressControllerBase
    {
        // GET: Address/Home
        public ActionResult Index()
        {
            var list = Core.ContactManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var contact = Core.ContactManager.Get(id);
            ViewBag.Contact = contact;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Contact contact)
        {
            if (contact.ID > 0)
            {
                if (!Core.ContactManager.Edit(contact))
                {
                    return ErrorJsonResult("编辑修改失败！未找到相关联系人信息！");
                }
            }
            else
            {
                var id = Core.ContactManager.Add(contact);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加新建联系人失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.ContactManager.Delete(id))
            {
                return ErrorJsonResult("删除联系人失败！");
            }
            return SuccessJsonResult();
        }
    }
}