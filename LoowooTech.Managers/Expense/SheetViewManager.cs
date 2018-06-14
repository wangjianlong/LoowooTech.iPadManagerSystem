using LoowooTech.Models;
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
            if (parameter.FinialUserId.HasValue)
            {
                query = query.Where(e => e.UserId.Value == parameter.FinialUserId.Value);
            }
            if (parameter.FlowDataState.HasValue)
            {
                query = query.Where(e => e.FlowDataState == parameter.FlowDataState.Value);
            }
            if (parameter.Year.HasValue)
            {
                query = query.Where(e => e.CheckTime.Value.Year == parameter.Year.Value);
            }
            if (parameter.Month.HasValue)
            {
                query = query.Where(e => e.CheckTime.Value.Month == parameter.Month.Value);
            }
            if (parameter.Day.HasValue)
            {
                query = query.Where(e => e.CheckTime.Value.Day == parameter.Day.Value);
            }
            query = query.ToList().AsQueryable();
            if (parameter.CheckUserId.HasValue)
            {
                
                query = query.Where(e => e.UserIds != null && e.UserIds.Any(j => j == parameter.CheckUserId));
            }
            query = query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
            return query.ToList();
        }

        public List<LWTime> GetTimes()
        {
            return DB.SheetViews.Where(e => e.State == Models.Admin.VerificationState.Success && e.FlowDataState == Models.Admin.FlowDataState.Done).GroupBy(e => new LWTime { Year = e.CheckTime.Value.Year, Month = e.CheckTime.Value.Month, Days = e.CheckTime.Value.Day }).Select(e => e.Key).OrderByDescending(e=>e.Year).ThenByDescending(e=>e.Month).ThenByDescending(e=>e.Days).ToList();
        }

        public Dictionary<LWTime,double> GetTimeDict()
        {
            return DB.SheetViews.Where(e => e.State == Models.Admin.VerificationState.Success && e.FlowDataState == Models.Admin.FlowDataState.Done).GroupBy(e => new LWTime { Year = e.CheckTime.Value.Year, Month = e.CheckTime.Value.Month, Days = e.CheckTime.Value.Day }).OrderByDescending(e => e.Key).ToDictionary(e => e.Key, e => Math.Round(e.Sum(i => i.Money), 2));
        }

    }
}
