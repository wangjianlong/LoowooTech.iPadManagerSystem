using LoowooTech.Common;
using System.Collections.Generic;
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
        [Column("UserIds")]
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
        [NotMapped]
        public List<User> Users { get; set; }
        public int? GroupId { get; set; }
        public int PrevId { get; set; }

        public string Remark { get; set; }

        [NotMapped]
        public FlowNode Next { get; set; }
    }
}
