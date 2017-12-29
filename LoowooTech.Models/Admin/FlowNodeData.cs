using LoowooTech.Common;
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
    [Table("flow_node_data")]
    public class FlowNodeData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public DateTime? CheckTime { get; set; }
        public int FlowNodeId { get; set; }
        public virtual FlowNode FlowNode { get; set; }
        public int FlowDataId { get; set; }
        public virtual FlowData FlowData { get; set; }
        public string UserIdValues { get; set; }
        [NotMapped]
        public int[] UserIds
        {
            get
            {
                if (string.IsNullOrEmpty(UserIdValues)) return null;
                return UserIdValues.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    UserIdValues = null;
                }
                else
                {
                    UserIdValues = string.Join(",", value);
                }
            }
        }
        /// <summary>
        /// 最终审核人员
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public VerificationState state { get; set; }
        public string Content { get; set; }
    }

    public enum VerificationState
    {
        [Description("待审核")]
        None,
        [Description("通过")]
        Success,
        [Description("不通过")]
        Fail,
        [Description("撤回")]
        Cancel
    }
}
