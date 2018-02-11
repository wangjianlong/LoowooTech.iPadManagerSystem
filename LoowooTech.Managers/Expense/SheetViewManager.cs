using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Expense
{
    public  class SheetViewManager:ManagerBase
    {
        public List<SheetFlowView> Search(SheetViewParameter parameter)
        {
            var query = DB.SheetViews.AsQueryable();
            if (parameter.SheetType.HasValue)
            {
                query = query.Where(e => e.SheetType == parameter.SheetType.Value);
            }
            if (parameter.SheetUserId.HasValue)
            {
                query = query.Where(e => e.SheetUserId == parameter.SheetUserId.Value);
            }
            if (parameter.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == parameter.CompanyId.Value);
            }
            if (parameter.State.HasValue)
            {
                query = query.Where(e => e.State == parameter.State.Value);
            }
            if (parameter.FLowNodeId.HasValue)
            {
                query = query.Where(e => e.FLowNodeId == parameter.FLowNodeId.Value);
            }
            query = query.ToList().AsQueryable();
            if (parameter.CheckUserId.HasValue)
            {
                
                query = query.Where(e => e.UserIds != null && e.UserIds.Any(j => j == parameter.CheckUserId));
            }
            query = query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
            return query.ToList();
        }
    }
}
