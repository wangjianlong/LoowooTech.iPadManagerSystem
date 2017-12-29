using LoowooTech.Models;
using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class FlowNodeDataManager:ManagerBase
    {
        public int Add(FlowNodeData data)
        {
            DB.FlowNodeDatas.Add(data);
            DB.SaveChanges();
            return data.ID;
        }

        public bool Edit(FlowNodeData data)
        {
            var model = DB.FlowNodeDatas.Find(data.ID);
            if (model == null)
            {
                return false;
            }
            data.CheckTime = DateTime.Now;
            DB.Entry(model).CurrentValues.SetValues(data);
            DB.SaveChanges();
            return true;
        }
        public bool Verification(FlowNodeData data,int userId)
        {
            var model = DB.FlowNodeDatas.Find(data.ID);
            if (model == null)
            {
                return false;
            }
            if (model.UserIds != null && model.UserIds.Contains(userId) == false)
            {
                return false;
            }
            model.UserId = userId;
            model.CheckTime = DateTime.Now;
            model.state = data.state;
            model.Content = data.Content;
            DB.SaveChanges();
            return true;
        }
        public bool Cancel(FlowData flowData,int userId)
        {
            var current = flowData.Current;
            if (current != null)
            {
                if (current.CheckTime.HasValue || current.state != VerificationState.None)
                {
                    return false;
                }

                current.state = VerificationState.Cancel;
                current.CheckTime = DateTime.Now;
                current.UserId = userId;


                flowData.Completed = false;
                flowData.State = FlowDataState.None;
                DB.SaveChanges();

                return true;
            }

            return false;
        }
   
        public bool Cancel(FlowData flowData,FlowNodeData prevnodeData)
        {
            var current = flowData.Current;
            if (current != null)
            {
                if (current.CheckTime.HasValue || current.state != VerificationState.None)
                {
                    return false;
                }
                current.state = VerificationState.Cancel;
                current.CheckTime = DateTime.Now;
                current.UserId = prevnodeData.UserId;
            }
            else
            {
                flowData.Completed = false;
            }
            var prevnode = flowData.Flow.GetPrevNode(current.FlowNodeId);
            if (prevnode == null)
            {
                flowData.State = FlowDataState.None;
            }
            else
            {
                DB.FlowNodeDatas.Add(new FlowNodeData
                {
                    FlowNodeId = prevnode.ID,
                    FlowDataId = flowData.ID,
                    UserIdValues=prevnode.UserIdValues
                    //UserId = prevnode.UserId
                });
            }
            DB.SaveChanges();
            return true;
        }

    }
}
