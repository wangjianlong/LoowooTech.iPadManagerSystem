using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class UserCompanyManager:ManagerBase
    {
        public void Update(List<UserCompany> list,int userId)
        {
            var old = DB.UserCompanys.Where(e => e.UserId == userId).ToList();
            if (old != null && old.Count > 0)
            {
                DB.UserCompanys.RemoveRange(old);
            }
            DB.UserCompanys.AddRange(list);
            DB.SaveChanges();
        }

        public List<UserCompany> Get(int userId)
        {
            return DB.UserCompanys.Where(e => e.UserId == userId).ToList();
        }

        public List<Company> GetCompanys(int userId)
        {
            var list = Get(userId);
            return list != null ? list.Select(e => e.Company).ToList() : new List<Company>();
        }
    }
}
