using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class DailyManager:ManagerBase
    {
        public Daily GetBySheetId(int SheetId)
        {
            return DB.Dailys.FirstOrDefault(e => e.SheetId == SheetId);
        }

        public int Add(Daily daily)
        {
            var model = DB.Dailys.FirstOrDefault(e => e.SheetId == daily.SheetId);
            if (model != null)
            {
                if (!Edit(daily))
                {
                    return 0;
                }
            }
            else
            {
                DB.Dailys.Add(daily);
                DB.SaveChanges();
            }
          
            return daily.ID;
        }

        public bool Edit(Daily daily)
        {
            var model = DB.Dailys.Find(daily.ID)??DB.Dailys.FirstOrDefault(e=>e.SheetId==daily.SheetId);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(daily);
            DB.SaveChanges();
            return true;
        }


    }
}
