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
            if (user.Delete == true)
            {
                return ErrorJsonResult("您当前无法登录系统，您的账号已经被管理员删除！");
            }
            if (user.UserRole != UserRole.Admin)
            {
                if (user.Companys == null || user.Companys.Count == 0)
                {
                    return ErrorJsonResult("管理员尚未给您设置团队，暂时无法登录系统！");
                }
            }
            var first = user.Companys.FirstOrDefault();
            if (first == null || first.Company == null)
            {
                return ErrorJsonResult("当前管理员给您指定的团队有误，请联系管理员！");
            }
            HttpContext.SaveAuth(user,first.Company);
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
        public ActionResult Register(User user,string Password2,bool? IsRead)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.LoginName))
            {
                return ErrorJsonResult("上传注册参数错误");
            }
            if (user.Password != Password2)
            {
                return ErrorJsonResult("两次输入密码不一致！请核对！");
            }
            if (IsRead == null || IsRead.Value == false)
            {
                return ErrorJsonResult("请务必勾选条例！");
            }
            if (Core.UserManager.ExistName(user.Name))
            {
                return ErrorJsonResult("系统中已经存在相同的真实名，请勿重复注册！");
            }
            if (Core.UserManager.ExistLoginName(user.LoginName))
            {
                return ErrorJsonResult("系统中已经存在相同的登录名，请更改！");
            }
            user.Password = user.Password.MD5();
            user.LogoPath = "Images/loowootech -head.png";
            var id = Core.UserManager.Add(user);
            if (id <= 0)
            {
                return ErrorJsonResult("注册用户失败！");
            }

            return SuccessJsonResult();
        }


        public ActionResult ChangeHead(int id)
        {
            var user = Core.UserManager.Get(id);
            return View();
        }


        public ActionResult SaveHead()
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请选择上传文件！");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            var ext = System.IO.Path.GetExtension(fileName);
            if (!FilePost.IsPictures(ext))
            {
                throw new ArgumentException(string.Format("当前格式{0}不支持", ext));
            }
            var info = FilePost.Upload(file, "Head", Identity.UserId);
            if (info == null)
            {
                throw new ArgumentException("上传文件失败！");
            }
            var fileId = Core.FileInfoManager.Add(info);
            if (fileId < 0)
            {
                throw new ArgumentException("保存文件信息失败！");
            }
            if (!Core.UserManager.ChangeHead(Identity.UserId, info.Path))
            {
                throw new ArgumentException("更改头像信息失败！");
            }
            var user = Core.UserManager.Get(Identity.UserId);
            if (user != null)
            {
                var first = user.Companys.FirstOrDefault();
                if (first != null && first.Company != null)
                {
                    HttpContext.SaveAuth(user, first.Company);
                }
              
            }
            return Redirect("/Home/Index");
        }
    }
}