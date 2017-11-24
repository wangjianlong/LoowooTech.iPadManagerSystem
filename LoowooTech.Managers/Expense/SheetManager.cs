using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class SheetManager:ManagerBase
    {
        public int Add(Sheet sheet)
        {
            DB.Sheets.Add(sheet);
            DB.SaveChanges();
            return sheet.ID;
        }
        public bool Edit(Sheet sheet)
        {
            var model = DB.Sheets.Find(sheet.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(sheet);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = DB.Sheets.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public Sheet Get(int id)
        {
            return DB.Sheets.Find(id);
        }

    }
}
