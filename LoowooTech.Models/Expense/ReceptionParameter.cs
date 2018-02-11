using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    public class ReceptionParameter:SheetParameter
    {
        public int ? CompanyId2 { get; set; }
        public int? UserId2 { get; set; }
        public string ItemContent { get; set; }
        public PayWay? Way { get; set; }
    }
}
