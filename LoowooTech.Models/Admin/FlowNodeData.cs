using System;
using System.Collections.Generic;
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
        public int FlowNodeId { get; set; }
        public virtual FlowNode FlowNode { get; set; }
        public int FlowDataId { get; set; }
        public int UserId { get; set; }
        public bool? Result { get; set; }
        public string Content { get; set; }
    }
}
