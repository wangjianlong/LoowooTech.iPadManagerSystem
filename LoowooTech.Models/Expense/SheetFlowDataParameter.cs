using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    public class SheetFlowDataParameter:ParameterBase
    {
        public int? UserId { get; set; }
        public SheetType? SheetType { get; set; }
        public FlowDataState? State { get; set; }
        public bool? IsCheck { get; set; }
        public bool? Delete { get; set; }
        public bool? Completed { get; set; }
    }
}
