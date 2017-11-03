using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class LWSystemManager:ManagerBase
    {
        public int Add(LWSystem system)
        {
            DB.LWSystems.Add(system);
            DB.SaveChanges();
            return system.ID;
        }

        public bool Delete(int id)
        {
            var model = DB.LWSystems.Find(id);
            if (model != null)
            {
                model.Delete = true;
                DB.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Edit(LWSystem system)
        {

        }
    }
}
