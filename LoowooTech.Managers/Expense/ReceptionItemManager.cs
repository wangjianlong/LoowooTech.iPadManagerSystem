using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class ReceptionItemManager:ManagerBase
    {
        public void Save(int receptionId,List<ReceptionItem> list)
        {
            var old = DB.ReceptionItems.Where(e => e.ReceptionId == receptionId).ToList();
            if (old != null && old.Count > 0)
            {
                DB.ReceptionItems.RemoveRange(old);
            }
            DB.ReceptionItems.AddRange(list);
            DB.SaveChanges();
        }
    }
}
