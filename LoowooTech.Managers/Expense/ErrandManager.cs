using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class ErrandManager:ManagerBase
    {
        public void Save(int evectionId,List<Errand> list)
        {
            var old = DB.Errands.Where(e => e.EvectionId == evectionId).ToList();
            if (old != null && old.Count > 0)
            {
                DB.Errands.RemoveRange(old);
            }

            DB.Errands.AddRange(list);
            DB.SaveChanges();
        }
    }
}
