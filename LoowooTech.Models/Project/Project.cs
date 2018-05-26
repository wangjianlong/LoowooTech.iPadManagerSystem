using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    [Table("project")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [NotMapped]
        public string Serial { get { return string.Format("P{0}{1}{2}{3}",CreateTime.Year,CreateTime.Month.ToString("00"),CreateTime.Day.ToString("00"),ID.ToString("0000")); } }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string SerialNumber { get; set; }
        public string Number { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public bool IsDone { get; set; }
        public string ReplyFile { get; set; }


        /// <summary>
        /// 项目类型
        /// </summary>
        public int? ProjectTypeId { get; set; }
        public virtual ProjectType ProjectType { get; set; }

        /// <summary>
        /// 甲方
        /// </summary>
        public int? CompanyAId { get; set; }

        public virtual Company CompanyA { get; set; }
        /// <summary>
        /// 乙方
        /// </summary>
        public int? CompanyBId { get; set; }
        public virtual Company CompanyB { get; set; }
    }
}
