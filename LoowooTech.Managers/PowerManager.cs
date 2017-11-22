using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class PowerManager:ManagerBase
    {
        public int Add(Power power)
        {
            DB.Powers.Add(power);
            DB.SaveChanges();
            return power.ID;
        }

        public Power Get(int id)
        {
            return DB.Powers.Find(id);
        }

        public bool Edit(Power Power)
        {
            var entry = DB.Powers.Find(Power.ID);
            if (entry != null)
            {
                DB.Items.RemoveRange(entry.Items);
                DB.Entry(entry).CurrentValues.SetValues(Power);
                entry.Items = Power.Items;
                DB.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var entry = DB.Powers.Find(id);
            if (entry != null)
            {
                entry.Delete = true;
                DB.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
