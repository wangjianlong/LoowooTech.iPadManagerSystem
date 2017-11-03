using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Common
{
    public class UserRoleAttribute:System.Web.Mvc.ActionFilterAttribute
    {
        public bool Mode { get; set; }
        public UserRole UserRole { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserRole == UserRole.Guest)
            {
                return;
            }
            var currentUser = (UserIdentity)Thread.CurrentPrincipal.Identity;
            if (Mode)
            {
                if (currentUser.UserRole != UserRole)
                {
                    throw new HttpException(401, "您没有权限查看此页面");
                }
            }
            else
            {
                if (currentUser.UserRole == UserRole)
                {
                    throw new HttpException(401, "您没有权限查看此页面");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}