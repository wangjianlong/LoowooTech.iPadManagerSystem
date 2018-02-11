using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
            if (model != null&&model.CityIds!=null)
            {
                model.Citys = Core.CityManager.GetList(model.CityIds);
            }
            return model;
        }

        public List<Evection> Search(EvectionParameter parameter)
        {
            var query = DB.Evections.Include(e => e.Errands).Include(e => e.Traffics).Include(e => e.Sheet).AsQueryable();
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Sheet.Delete == parameter.Delete.Value);
            }
            if (parameter.CompanyId.HasValue)
            {
                query = query.Where(e => e.Sheet.CompanyId == parameter.CompanyId.Value);
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.Sheet.UserId == parameter.UserId.Value);
            }

            if (parameter.ProjectId.HasValue)
            {
                query = query.Where(e => e.Sheet.ProjectId == parameter.ProjectId.Value);
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.Sheet.Time >= parameter.StartTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Sheet.Time <= parameter.EndTime.Value);
            }
            if (!string.IsNullOrEmpty(parameter.Content))
            {
                query = query.Where(e => e.Sheet.Remark.Contains(parameter.Content));
            }
            if (parameter.Vehicales.HasValue)
            {
                query = query.Where(e => e.Traffics.Any(j => j.Vehicles == parameter.Vehicales.Value));
            }
            if (parameter.UserId2.HasValue)
            {
                query = query.Where(e => e.Errands.Any(j => j.UserId == parameter.UserId2.Value));
            }
            if (parameter.CityId.HasValue)
            {
                query = query.ToList().Where(e => e.CityIds.Contains(parameter.CityId.Value)).AsQueryable();
            }
            query = query.OrderByDescending(e => e.Sheet.Time).SetPage(parameter.Page);
            var list = query.ToList();
            foreach(var item in list)
            {
                if (item.CityIds != null)
                {
                    item.Citys = Core.CityManager.GetList(item.CityIds);
                }
               
            }
            return list;
        }

    }
}
