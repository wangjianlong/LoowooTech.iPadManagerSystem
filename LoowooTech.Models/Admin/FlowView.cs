using LoowooTech.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Admin
{
    [Table("flow_view")]
    public class FlowView
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int FlowNodeId { get; set; }
        /// <summary>
        /// flow_node: Name
        /// </summary>
        public string FlowNodeName { get; set; }
        public int FlowDataId { get; set; }
        public int FlowId { get; set; }
        public string FlowName { get; set; }
        public bool CanBack { get; set; }
        public bool FlowDelete { get; set; }
        public bool Completed { get; set; }
        public FlowDataState FlowDataState { get; set; }
        public string UserIdValues { get; set; }
        [NotMapped]
        public int[] UserIds
        {
            get
            {
                if (string.IsNullOrEmpty(UserIdValues)) return null;
                return UserIdValues.ToIntArray();
            }
        }
        public int? UserId { get; set; }
        public VerificationState State { get; set; }
        public string Content { get; set; }
        public DateTime? CheckTime { get; set; }
    }
}
