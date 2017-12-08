using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class EvectionManager:ManagerBase
    {
        public int Add(Evection evection)
        {
            var model = DB.Evections.FirstOrDefault(e => e.SheetId == evection.SheetId);
            if (model != null)
            {
                if (!Edit(evection))
                {
                    return 0;
                }
            }
            else
            {
                DB.Evections.Add(evection);
                DB.SaveChanges();
            }
      
            return evection.ID;
        }

        public bool Edit(Evection evection)
        {
            var model = DB.Evections.Find(evection.ID)??DB.Evections.FirstOrDefault(e=>e.SheetId==evection.SheetId);
            if (model == null)
            {
                return false;
            }
            evection.ID = model.ID;
            DB.Entry(model).CurrentValues.SetValues(evection);
            DB.SaveChanges();
            return true;
        }

        public Evection GetBySheetId(int sheetId)
        {
            var model = DB.Evections.FirstOrDefault(e => e.SheetId == sheetId);
            if (model != null)
            {
                model.Citys = Core.CityManager.GetList(model.CityIds);
            }
            return model;
        }

    }
}
