using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
                if (model.CompanyIds != null)
                {
                    model.Companys = Core.CompanyManager.Get(model.CompanyIds);
                }
                if (model.UserIds != null)
                {
                    model.Users = Core.UserManager.Get(model.UserIds);
                }
               
               
            }
            return model;
        }

        public List<Reception> Search(ReceptionParameter parameter)
        {
            var query = DB.Receptions.Include(e => e.Items).Include(e=>e.Sheet).AsQueryable();
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Sheet.Delete == parameter.Delete.Value);
            }
            if (parameter.CompanyId.HasValue)
            {
                query = query.Where(e => e.Sheet.CompanyId == parameter.CompanyId.Value);
            }

            if (parameter.ProjectId.HasValue)
            {
                query = query.Where(e => e.Sheet.ProjectId == parameter.ProjectId.Value);
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.Sheet.UserId == parameter.UserId.Value);
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
            if (parameter.Way.HasValue)
            {
                query = query.Where(e => e.Items.Any(j => j.PayWay == parameter.Way.Value));
            }
            if (!string.IsNullOrEmpty(parameter.ItemContent))
            {
                query = query.Where(e => e.Items.Any(j => j.Content.Contains(parameter.ItemContent)));
            }
            if (parameter.CompanyId2.HasValue || parameter.UserId2.HasValue)
            {
                query = query.ToList().AsQueryable();
                if (parameter.CompanyId2.HasValue)
                {
                    query = query.Where(e => e.CompanyIds.Contains(parameter.CompanyId2.Value));
                }
                if (parameter.UserId2.HasValue)
                {
                    query = query.Where(e => e.UserIds.Contains(parameter.UserId2.Value));
                }
            }
            query = query.OrderByDescending(e => e.Sheet.Time).SetPage(parameter.Page);
            var list = query.ToList();
            foreach(var item in list)
            {
                if (item.CompanyIds != null)
                {
                    item.Companys = Core.CompanyManager.Get(item.CompanyIds);
                }
                if (item.UserIds != null)
                {
                    item.Users = Core.UserManager.Get(item.UserIds);
                }
            }
            return list;
        }
    }
}
