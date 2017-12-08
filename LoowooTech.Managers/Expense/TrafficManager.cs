using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class TrafficManager:ManagerBase
    {
        public void Save(int evectionId,List<Traffic> list)
        {
            var old = DB.Traffics.Where(e => e.EvectionId == evectionId).ToList();
            if (old != null && old.Count > 0)
            {
                DB.Traffics.RemoveRange(old);
            }

            //foreach(var item in list)
            //{
            //    item.EvectionId = evectionId;
            //    DB.Traffics.Add(item);
            //}


            DB.Traffics.AddRange(list);
            DB.SaveChanges();
        }
    }
}
