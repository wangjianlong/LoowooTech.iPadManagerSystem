using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class AccountManager:ManagerBase
    {
        public Account Get(int id)
        {
            return DB.Accounts.Find(id);
        }

        public List<Account> GetList()
        {
            return DB.Accounts.Where(e => e.Delete == false).ToList();
        }

        public int Add(Account account)
        {
            DB.Accounts.Add(account);
            DB.SaveChanges();
            return account.ID;
        }

        public bool Delete(int id,bool flag = true)
        {
            var model = DB.Accounts.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }

        public bool Edit(Account account)
        {
            var model = DB.Accounts.Find(account.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(account);
            DB.SaveChanges();
            return true;
        }
    }
}
