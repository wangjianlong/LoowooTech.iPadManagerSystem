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

        public User Get(int id)
        {
            return DB.Users.Find(id);
        }
        public List<User> Get(int[] Ids)
        {
            var list = new List<User>();
            foreach(var id in Ids)
            {
                var model = Get(id);
                if (model != null)
                {
                    list.Add(model);
                }
            }
            return list;
        }

        public List<User> GetList()
        {
            return DB.Users.Where(e => e.Delete == false).OrderByDescending(e => e.RegisterTime).ToList();
        }

        public bool Delete(int id)
        {
            var model = DB.Users.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();

            return true;
        }

        public int Add(User user)
        {
            DB.Users.Add(user);
            DB.SaveChanges();
            return user.ID;
        } 

        public bool Edit(User user)
        {
            var model = DB.Users.Find(user.ID);
            if (model == null)
            {
                return false;
            }
            user.Password = model.Password;//密码不做修改操作！
            DB.Entry(model).CurrentValues.SetValues(user);
            DB.SaveChanges();
            return true;
        }
    }
}
