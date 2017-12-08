using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class SubstanceManager:ManagerBase
    {
        public void Save(int dailyId,List<Substance> list)
        {
            var old = DB.Substances.Where(e => e.DailyId == dailyId).ToList();
            if (old != null && old.Count > 0)
            {
                DB.Substances.RemoveRange(old);
            }

            DB.Substances.AddRange(list);
            DB.SaveChanges();
        }
    }
}
