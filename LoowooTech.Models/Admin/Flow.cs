using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LoowooTech.Models.Admin
{
    [Table("flow")]
    public class Flow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name { get; set; }
        public bool CanBack { get; set; }
        public bool Delete { get; set; }
        public string Remark { get; set; }
        public int LWSystemId { get; set; }
        public virtual LWSystem LWSystem { get; set; }
        public virtual List<FlowNode> FlowNodes { get; set; }

        public FlowNode GetFirstNode()
        {
            if (FlowNodes == null)
            {
                return null;
            }
            var node = FlowNodes.FirstOrDefault(e => e.PrevId == 0);
            if (node != null)
            {
                node.Next = GetNextNode(node.ID);
            }
            return node;
        }
        public FlowNode GetNextNode(int id)
        {
            var node = FlowNodes.FirstOrDefault(e => e.PrevId == id);
            if (node != null)
            {
                node.Next = GetNextNode(node.ID);
            }
            return node;
        }
        public FlowNode GetLastNode()
        {
            if (FlowNodes == null)
            {
                return null;//说明当前流程还没有配置节点
            }
            foreach(var node in FlowNodes)
            {
                if (!FlowNodes.Any(e => e.PrevId == node.ID))
                {
                    return node;
                }
            }
            throw new Exception("配置节点信息错误！");
        }
    }
}
