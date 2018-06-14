using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    /// <summary>
    /// 工作记录
    /// </summary>
    [Table("workschedule")]
    public class WorkSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public bool Delete { get; set; }
        public virtual List<WorkScheduleFiles> Files { get; set; }

    }

    [Table("WorkSchedule_file")]
    public class WorkScheduleFiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int WorkScheduleId { get; set; }
        public virtual WorkSchedule WorkSchedule { get; set; }
        public int FileId { get; set; }
        public virtual FileInfo FileInfo { get; set; }
        public bool Delete { get; set; }
    }
}
