using LoowooTech.Models;
using LoowooTech.Models.Admin;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class FlowDataManager:ManagerBase
    {
        public bool Exist(int flowId,int infoId)
        {
            return DB.FlowDatas.Any(e => e.FlowId == flowId && e.InfoId == infoId);
        }

        public int Add(FlowData flowData)
        {
            DB.FlowDatas.Add(flowData);
            DB.SaveChanges();
            return flowData.ID;
        }
        /// <summary>
        /// 作用：获取
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public FlowData Get(int flowId,int infoId)
        {
            var data= DB.FlowDatas.FirstOrDefault(e => e.FlowId == flowId && e.InfoId == infoId);
            if (data == null)
            {
                data = new FlowData { FlowId = flowId, InfoId = infoId };
                var id = Add(data);
            }
            return data;
        }
        public FlowData Get(int id)
        {
            return DB.FlowDatas.Find(id);
        }
        public bool Change(int id,FlowDataState state)
        {
            var model = DB.FlowDatas.Find(id);
            if (model == null)
            {
                return false;
            }
            model.State = state;
            //model.Completed = flag;
            DB.SaveChanges();
            return true;
        }
        public List<FlowData> Search(FlowDataParameter parameter)
        {
            var query = DB.FlowDatas.Include(e=>e.NodeDatas).AsQueryable();
            if (parameter.FlowId.HasValue)
            {
                query = query.Where(e => e.FlowId == parameter.FlowId.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.Completed == parameter.Completed.Value);
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.NodeDatas.Any(j => j.UserIds.Contains(parameter.UserId.Value)));
                //query = query.Where(e => e.NodeDatas.Any(j => j.UserId == parameter.UserId.Value));
            }
            if (parameter.State.HasValue)
            {
                query = query.Where(e => e.NodeDatas.Any(j => j.state == parameter.State.Value));
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.NodeDatas.Any(j => j.Time >= parameter.StartTime.Value));
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.NodeDatas.Any(j => j.Time <= parameter.EndTime.Value));
            }
            query = query.OrderByDescending(e => e.InfoId).SetPage(parameter.Page);
            return query.ToList();
        }
    }
}
