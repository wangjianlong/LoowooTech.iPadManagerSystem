using LoowooTech.Models.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models.Expense
{
    [Table("sheet_flowData_view")]
    public class SheetFlowDataView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        [NotMapped]
        public string SerialNumber { get { return string.Format("E{0}{1}{2}{3}", Time.Year, Time.Month.ToString("00"), Time.Day.ToString("00"), ID.ToString("0000")); } }
        public int Count { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SheetType SheetType { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Delete { get; set; }
        public double Money { get; set; }
        public bool IsCheck { get; set; }
        public DateTime? CheckTime { get; set; }
        public bool Completed { get; set; }
        public FlowDataState State { get; set; }
    }
}
