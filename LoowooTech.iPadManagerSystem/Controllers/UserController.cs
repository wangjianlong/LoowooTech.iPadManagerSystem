using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.iPadManagerSystem.Common;

namespace LoowooTech.iPadManagerSystem.Controllers
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
        public ActionResult Login(string name, string password)
        {
            var user = Core.UserManager.Login(name, password);
            if (user == null)
            {
                return ErrorJsonResult("登录失败！请核对用户名和密码");
            }
            if (user.Approve == false)
            {
                return ErrorJsonResult("当前用户未通过管理员授权，无法登录系统");
            }
            HttpContext.SaveAuth(user);
            return SuccessJsonResult();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string name,string password,string displayName,string code)
        {
            var queryCode = System.Configuration.ConfigurationManager.AppSettings["Code"];
            if (queryCode != null && queryCode.ToString() != code)
            {
                return ErrorJsonResult("注册码不正确，请联系公司管理人员！");
            }
            if (Core.UserManager.Exist(name))
            {
                return ErrorJsonResult("系统中已存在相同登录名");
            }
            if (string.IsNullOrEmpty(displayName)||string.IsNullOrEmpty(password))
            {
                return ErrorJsonResult("用户名以及密码不能为空");
            }
            try
            {
                Core.UserManager.Register(name, displayName, password);
            }catch(Exception ex)
            {
                return ErrorJsonResult(string.Format("注册失败，失败信息：{0},详细信息：{1}", ex.Message, ex.ToString()));
            }
            return SuccessJsonResult();
        }

        public ActionResult LoginOut()
        {
            HttpContext.ClearAuth();
            return RedirectToAction("Login");
        }
    }
}