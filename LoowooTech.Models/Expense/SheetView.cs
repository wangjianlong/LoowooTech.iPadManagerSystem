using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("Sheet_view")]
    public class SheetView
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int Count { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SheetType SheetType { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Delete { get; set; }
        public double Money { get; set; }
        public bool IsCheck { get; set; }
        public DateTime? CheckTime { get; set; }
        public int? FlowDataId { get; set; }
    }
}
