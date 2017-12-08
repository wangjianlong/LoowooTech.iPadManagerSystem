using LoowooTech.Common;
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
        public Sheet GetSingle(int id)
        {
            return DB.Sheets.Find(id);
        }

        public Sheet Get(int id)
        {
            var model = DB.Sheets.Find(id);
            if (model != null)
            {
                switch (model.SheetType)
                {
                    case SheetType.Evection:
                        model.Evection = Core.EvectionManager.GetBySheetId(model.ID);
                        break;
                    case SheetType.Daily:
                        model.Daily = Core.DailyManager.GetBySheetId(model.ID);
                        break;
                    case SheetType.Reception:
                        model.Reception = Core.ReceptionManager.GetBySheetId(model.ID);
                        break;
                }
            }
            return model;
        }
        public List<Sheet> Search(SheetParameter parameter)
        {
            var query = DB.Sheets.Where(e => e.Delete == false).AsQueryable();
            if (parameter.Type.HasValue)
            {
                query = query.Where(e => e.SheetType == parameter.Type.Value);
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }
            if (parameter.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == parameter.CompanyId.Value);
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.Time >= parameter.StartTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Time <= parameter.EndTime.Value);
            }
            if (!string.IsNullOrEmpty(parameter.Content))
            {
                query = query.Where(e => e.Remark.Contains(parameter.Content));
            }
            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
            return query.ToList();
        }

    }
}
