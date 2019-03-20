using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class TabletManager:ManagerBase
    {
        public Tablet Get(int id)
        {
            return DB.Tablets.Find(id);
        }
        public int Add(Tablet tablet)
        {
            DB.Tablets.Add(tablet);
            DB.SaveChanges();
            return tablet.ID;
        }

        public  bool Delete(int id,bool flag=true)
        {
            var model = DB.Tablets.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }

        public bool Edit(Tablet tablet)
        {
            var model = DB.Tablets.Find(tablet.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(tablet);
            DB.SaveChanges();

            return true;
        }



        public List<Tablet> Search(TabletParameter parameter)
        {
            var query = DB.Tablets.AsQueryable();
            if (!string.IsNullOrEmpty(parameter.SerialNumer))
            {
                query = query.Where(e => e.SerialNumber.Contains(parameter.SerialNumer));
            }
            if (parameter.BuyerId.HasValue)
            {
                query = query.Where(e => e.BuyerId == parameter.BuyerId.Value);
            }
            if (parameter.AccountId.HasValue)
            {
                query = query.Where(e => e.AccountId == parameter.AccountId.Value);
            }
 
            if (parameter.TypeId.HasValue)
            {
                query = query.Where(e => e.TypeId == parameter.TypeId.Value);
            }
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Delete == parameter.Delete.Value);
            }
            query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
            return query.ToList();
        }
        public List<Tablet> GetUse()
        {
            return DB.Tablets.Where(e => e.Delete == false).ToList().Where(e=>e.CanUse==true).ToList();
        }

        public bool Exist(string serialNumber,int id)
        {
            return DB.Tablets.Any(e => e.SerialNumber == serialNumber && e.ID != id); 
        }
    }
}
