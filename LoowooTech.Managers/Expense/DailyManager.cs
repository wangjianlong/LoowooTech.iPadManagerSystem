using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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


        public List<Daily> Search(DailyParameter parameter)
        {
            var query = DB.Dailys.Include(e=>e.Sheet).Include(e=>e.Substances).AsQueryable();
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Sheet.Delete == parameter.Delete.Value);
            }
            if (parameter.CategoryId.HasValue)
            {
                query = query.Where(e => e.Substances.Any(j => j.CategoryId == parameter.CategoryId.Value));
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.Sheet.UserId == parameter.UserId.Value);
            }
            if (parameter.CompanyId.HasValue)
            {
                query = query.Where(e => e.Sheet.CompanyId == parameter.CompanyId.Value);
            }

            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Time <= parameter.EndTime.Value);
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.Time >= parameter.StartTime.Value);
            }
            if (parameter.MinCost.HasValue)
            {
                query = query.Where(e => e.Substances.Any(j => j.Cost >= parameter.MinCost.Value));
            }
            if (parameter.MaxCost.HasValue)
            {
                query = query.Where(e => e.Substances.Any(j => j.Cost <= parameter.MaxCost.Value));
            }
            if (!string.IsNullOrEmpty(parameter.Content))
            {
                query = query.Where(e => e.Sheet.Remark.Contains(parameter.Content));
            }
            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
            return query.ToList();
        }

    }
}
