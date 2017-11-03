using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LoowooTech.Offical.Web.Common
{
    public static class AuthUtils
    {
        private const string _cookieName = "loowooTech.user";
        public static string GenerateToken(this HttpContextBase context,User user)
        {
            var ticket = new FormsAuthenticationTicket(1, user.ID + "|" + user.LoginName + "|" + user.Name + "|" + user.GroupId + "|" + user.RoleId,DateTime.Now,DateTime.MaxValue,true,"LoowooTech.Office_user_token");
            var token = FormsAuthentication.Encrypt(ticket);
            return token;
        }

        public static void SaveAuth(this HttpContextBase context,User user)
        {
            user.AccessToken = GenerateToken(context, user);
            var cookie = new HttpCookie(_cookieName, user.AccessToken);
            context.Response.Cookies.Remove(_cookieName);
            context.Response.Cookies.Add(cookie);
        }

        private static string GetToken(HttpContextBase context)
        {
            var token = context.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                var cookie = context.Request.Cookies.Get(_cookieName);
                if (cookie != null)
                {
                    token = cookie.Value;
                }
            }
            return token;
        }


        public static UserIdentity GetCurrentUser(this HttpContextBase context)
        {
            var token = GetToken(context);
            if (!string.IsNullOrEmpty(token))
            {
                var ticket = FormsAuthentication.Decrypt(token);
                if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                {
                    var values = ticket.Name.Split('|');
                    if (values.Length == 5)
                    {
                        return new UserIdentity
                        {
                            UserId = int.Parse(values[0]),
                            LoginName = values[1],
                            Name = values[2],
                            GroupId = int.Parse(values[3]),
                            RoleId = int.Parse(values[4])
                        };
                    }
                }
            }
            return UserIdentity.Guest;
            
        }


        public static void ClearAuth(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies.Get(_cookieName);
            if (cookie == null) return;
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddHours(2);
            context.Response.SetCookie(cookie);
        }

    }
}