using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Common
{
    public class UserAuthorizeAttribute:System.Web.Mvc.AuthorizeAttribute
    {
        private UserRole _role { get; set; }
        private bool _enable { get; set; }
        public UserAuthorizeAttribute(bool enable = true)
        {
            _enable = enable;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_enable)
            {
                var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                filterContext.HttpContext.Response.Redirect("/User/Login?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
            }
        }

    }
}