using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models.Admin
{
    [Table("flow_node")]
    public class FlowNode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int FlowId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? GroupId { get; set; }
        public int PrevId { get; set; }

        public string Remark { get; set; }

        [NotMapped]
        public FlowNode Next { get; set; }
    }
}
