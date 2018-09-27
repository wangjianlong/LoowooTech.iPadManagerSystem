using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Admin
{
    [Table("flow_data")]
    public class FlowData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? InfoId { get; set; }
        public int FlowId { get; set; }
        public virtual Flow Flow { get; set; }
        public FlowDataState State { get; set; }
        public bool Completed { get; set; }
        public virtual List<FlowNodeData> NodeDatas { get; set; }
        public FlowNodeData GetNodeByUserId(int userId)
        {
            if (NodeDatas == null) return null;
            return NodeDatas.Where(e => e.UserId == userId).OrderByDescending(e => e.ID).FirstOrDefault();
        }
        public FlowNodeData GetNextNodeData(int currentNodeDataId)
        {
            if (NodeDatas == null) return null;
            return NodeDatas.Where(e => e.ID > currentNodeDataId).OrderBy(e => e.ID).FirstOrDefault();
        }
        public FlowNodeData GetSkipNode(int skipId)
        {
            return NodeDatas.OrderByDescending(e => e.ID).Skip(skipId).FirstOrDefault();
        }
        public bool CanCancel(int userId,int ownId)
        {
            if (Flow.CanBack == false)
            {
                return false;
            }
            if (Current == null)
            {
                return false;
            }
            var prev = NodeDatas.OrderByDescending(e => e.ID).Skip(1).FirstOrDefault();
            if (prev == null)
            {
                return userId == ownId;
            }
            else
            {
                if (prev.FlowNode != null && prev.FlowNode.PrevId == 0)
                {
                    return userId == ownId;
                }
            }
            return prev.UserId == userId;
        }

        public FlowNodeData Current
        {
            get
            {
                return NodeDatas == null ? null : NodeDatas.OrderByDescending(e => e.ID).FirstOrDefault();
            }
        }

    }

    public enum FlowDataState
    {
        [Description("未审核")]
        None,
        [Description("审核中")]
        Checking,
        [Description("完成")]
        Done
    }
}
