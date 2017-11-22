using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var list = Core.UserManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id=0)
        {
            var user = Core.UserManager.Get(id);
            ViewBag.User = user;
            var groups = Core.GroupManager.GetList();
            ViewBag.Groups = groups;
            return View();
        }

        [HttpPost]
        public ActionResult Save(User user)
        {
            if (user.ID > 0)
            {
                if (!Core.UserManager.Edit(user))
                {
                    return ErrorJsonResult("编辑修改用户信息失败！未找到相关用户信息！");
                }
            }
            else
            {
                var id = Core.UserManager.Add(user);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加用户失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.UserManager.Delete(id))
            {
                return ErrorJsonResult("删除用户失败!");
            }
            return SuccessJsonResult();
        }
    }
}