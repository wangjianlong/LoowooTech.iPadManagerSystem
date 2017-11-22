using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models.Admin
{
    [Table("project_type")]
    public class ProjectType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public int? ParentId { get; set; }
        [NotMapped]
        public List<ProjectType> Children { get; set; }
        [NotMapped]
        public string ParentName { get; set; }
    }
}
