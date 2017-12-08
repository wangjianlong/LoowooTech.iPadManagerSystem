using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class ReceptionManager:ManagerBase
    {
        public int Add(Reception reception)
        {
            var model = DB.Receptions.FirstOrDefault(e => e.SheetId == reception.SheetId);
            if (model != null)
            {
                if (!Edit(reception))
                {
                    return 0;
                }
            }
            else
            {
                DB.Receptions.Add(reception);
                DB.SaveChanges();
            }
    
            return reception.ID;
        }

        public bool Edit(Reception reception)
        {
            var model = DB.Receptions.Find(reception.ID)??DB.Receptions.FirstOrDefault(e=>e.SheetId==reception.SheetId);
            if (model == null)
            {
                return false;
            }
            reception.ID = model.ID;
            DB.Entry(model).CurrentValues.SetValues(reception);
            DB.SaveChanges();
            return true;
        }
        public Reception GetBySheetId(int sheetId)
        {
            var model = DB.Receptions.FirstOrDefault(e => e.SheetId == sheetId);
            if (model != null)
            {
                model.Companys = Core.CompanyManager.Get(model.CompanyIds);
                model.Users = Core.UserManager.Get(model.UserIds);
            }
            return model;
        }
    }
}
