using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    public class DailyParameter:SheetParameter
    {
        public int? CategoryId { get; set; }
        public double? MinCost { get; set; }
        public double ? MaxCost { get; set; }
    }
}
