using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public class SheetFlowDataViewManager:ManagerBase
    {
        public List<SheetFlowDataView> Search(SheetFlowDataParameter parameter)
        {
            var query = DB.SheetFlowDataViews.AsQueryable();
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Delete == parameter.Delete.Value);
            }

            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }

            if (parameter.SheetType.HasValue)
            {
                query = query.Where(e => e.SheetType == parameter.SheetType.Value);
                
            }
            if (parameter.State.HasValue)
            {
                query = query.Where(e => e.State == parameter.State.Value);
            }

            if (parameter.IsCheck.HasValue)
            {
                query = query.Where(e => e.IsCheck == parameter.IsCheck.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.Completed == parameter.Completed.Value);
            }
            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
            return query.ToList();
        }
    }
}
