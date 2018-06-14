using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    [Table("project_user")]
    public class ProjectUser
    {
        [Key]
        public int ID { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ProjectRelation Relation { get; set; }
    }

    public enum ProjectRelation
    {
        [Description("参与")]
        Participation,
        [Description("负责")]
        InCharge
    }
}
