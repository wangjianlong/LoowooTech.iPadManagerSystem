using LoowooTech.Common;
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
            User user = null;
#if DEBUG
            user = Core.UserManager.Get(loginName);
#else
            user = Core.UserManager.Get(loginName, password);
#endif

            if (user == null)
            {
                user = Core.UserManager.Get(loginName);
                if (user == null)
                {
                    return ErrorJsonResult("登录失败，未获取相关用户信息");
                }
                else
                {
                    if (string.IsNullOrEmpty(user.Password))
                    {
                        return WarningJsonResult(user.ID);
                        //return RedirectToAction("Password", new { id = user.ID });
                    }
                    return ErrorJsonResult("登录失败，请核对输入的密码");
                }
              
            }
            HttpContext.SaveAuth(user);
            return SuccessJsonResult();
        }


        public ActionResult Password(int id)
        {
            var user = Core.UserManager.Get(id);
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult Password(int id, string oldPassword,string newPassword,string RePassword)
        {
            if (newPassword != RePassword)
            {
                return ErrorJsonResult("输入的两次新密码不一致！");
            }
            var user = Core.UserManager.Get(id);
            if (user == null)
            {
                return ErrorJsonResult("参数错误");
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                if (user.Password != oldPassword.MD5())
                {
                    return ErrorJsonResult("原始密码不正确");
                }
            }
 
            if (!Core.UserManager.Password(id, newPassword))
            {
                return ErrorJsonResult("设置新密码失败");
            }
            AuthUtils.ClearAuth(HttpContext);
            return SuccessJsonResult();
        }


        public ActionResult LogOut()
        {
            AuthUtils.ClearAuth(HttpContext);
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