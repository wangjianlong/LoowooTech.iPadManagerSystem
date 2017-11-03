using LoowooTech.Common;
using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class UserManager:ManagerBase
    {
        public User Get(string loginName,string password)
        {
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            password = password.MD5();
            return DB.Users.FirstOrDefault(e => e.LoginName.ToLower() == loginName.ToLower() && e.Password.ToLower() == password.ToLower());
        }
    }
}
