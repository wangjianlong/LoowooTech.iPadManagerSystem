using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class TabletTypeManager:ManagerBase
    {
        public TabletType Get(int id)
        {
            return DB.TabletTypes.Find(id);
        }

        public List<TabletType> GetList()
        {
            return DB.TabletTypes.Where(e => e.Delete == false).ToList();
        }

        public int Add(TabletType type)
        {
            DB.TabletTypes.Add(type);
            DB.SaveChanges();
            return type.ID;
        }

        public bool Edit(TabletType type)
        {
            var model = DB.TabletTypes.Find(type.ID);
            if (model == null)
            {
                return false;
            }
            type.UserId = model.UserId;

            DB.Entry(model).CurrentValues.SetValues(type);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id,bool flag=true)
        {
            var model = DB.TabletTypes.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }


    }
}
