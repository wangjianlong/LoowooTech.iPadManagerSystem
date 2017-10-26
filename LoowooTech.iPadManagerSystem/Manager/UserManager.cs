using LoowooTech.iPadManagerSystem.Common;
using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class UserManager:ManagerBase
    {
        /// <summary>
        /// 作用：用户登录  密码为明文  返回用户
        /// 作者：汪建龙
        /// 编写时间：2017年2月9日10:32:54
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string name,string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            password = password.MD5();
            using (var db = GetDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Name.ToLower() == name.ToLower() && e.PassWord.ToLower() == password.ToLower());

                return user;
            }
        }
        /// <summary>
        /// 作用：验证系统中是否已存在  登录名不区分大小写
        /// 作者：汪建龙
        /// 编写时间：2017年2月9日11:25:28
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            using (var db = GetDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
                return user != null;
            }
        }
        /// <summary>
        /// 作用：
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        public void Register(string name,string displayName,string password)
        {
            using (var db = GetDbContext())
            {
                db.Users.Add(new User()
                {
                    Name = name,
                    DisplayName = displayName,
                    PassWord = password.MD5()
                });
                db.SaveChanges();   
            }
        }
    }
}