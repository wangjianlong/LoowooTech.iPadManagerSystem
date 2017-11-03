using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LoowooTech.Offical.Web.Common
{
    public class UserPrincipal:IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

    }


    public class UserIdentity : IIdentity
    {
        public static readonly UserIdentity Guest = new UserIdentity { UserRole = UserRole.Guest };
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public string AuthenticationType { get { return "Web.Session"; } }
        public bool IsAuthenticated { get { return UserId > 0; } }
    }
}