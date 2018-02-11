using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    public class SheetParameter:ParameterBase
    {
        public SheetType? Type { get; set; }
        public int? CompanyId { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Content { get; set; }
        public bool? Delete { get; set; }
    }
}
