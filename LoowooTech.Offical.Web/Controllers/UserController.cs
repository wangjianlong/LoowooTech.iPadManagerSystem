using LoowooTech.Models;
using LoowooTech.Offical.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Controllers
{
    [UserAuthorize(false)]
    public class UserController : ControllerBase
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginName,string password)
        {
            var user = Core.UserManager.Get(loginName, password);
            if (user == null)
            {
                return ErrorJsonResult("登录失败，未获取相关用户信息");
            }
            HttpContext.SaveAuth(user);
            return SuccessJsonResult();
        }


        public ActionResult LogOut()
        {
            HttpContext.ClearAuth();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            return SuccessJsonResult();
        }
    }
}