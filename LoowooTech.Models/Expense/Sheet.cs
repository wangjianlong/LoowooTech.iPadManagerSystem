using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("sheet")]
    public class Sheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        [NotMapped]
        public string SerialNumber { get { return string.Format("E{0}{1}{2}{3}", Time.Year, Time.Month.ToString("00"), Time.Day.ToString("00"), ID.ToString("0000")); } }
        /// <summary>
        /// 发票张数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public SheetType SheetType { get; set; }
        public int ProjectId { get; set; }
        public virtual Project.Project Project { get; set; }
        [NotMapped]
        public FlowData FlowData { get; set; }
        [NotMapped]
        public Evection Evection { get; set; }
        [NotMapped]
        public Daily Daily { get; set; }
        [NotMapped]
        public Reception Reception { get; set; }
        public double Money { get; set; }

        public bool Delete { get; set; }
        /// <summary>
        /// 确认完成
        /// </summary>
        public bool IsCheck { get; set; }
        public DateTime? CheckTime { get; set; }

    }

    public enum SheetType
    {
        [Description("日常报销")]
        Daily,
        [Description("出差报销")]
        Evection,
        [Description("招待报销")]
        Reception,
        //[Description("其他")]
        //Other
    }
}
