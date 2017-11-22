using LoowooTech.Models.Admin;
using System.Collections.Generic;
using System.Linq;

namespace LoowooTech.Managers.Admin
{
    public class CompanyManager:ManagerBase
    {
        public int Add(Company company)
        {
            DB.Companys.Add(company);
            DB.SaveChanges();
            return company.ID;
        }

        public bool Edit(Company company)
        {
            var model = DB.Companys.Find(company.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(company);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = DB.Companys.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public Company Get(int id)
        {
            return DB.Companys.Find(id);
        }


        public List<Company> GetList()
        {
            return DB.Companys.Where(e => e.Delete == false&&e.IsProject==null).OrderBy(e => e.ID).ToList();
        }

        public List<Company> GetProjects()
        {
            return DB.Companys.Where(e => e.Delete == false && e.IsProject == true).OrderBy(e => e.ID).ToList();
        }
    }
}
