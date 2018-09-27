using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("flow_sheet_view")]
    public class FlowSheetView:FlowView
    {
        public int SheetId { get; set; }
        public DateTime SheetTime { get; set; }

        [NotMapped]
        public string SerialNumber { get { return string.Format("E{0}{1}{2}{3}", SheetTime.Year, SheetTime.Month.ToString("00"), SheetTime.Day.ToString("00"), SheetId.ToString("0000")); } }
        public int Count { get; set; }
        public string Remark { get; set; }
        public int SheetUserId { get; set; }
        /// <summary>
        /// 报销单人名
        /// </summary>
        public string UserName { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SheetType SheetType { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Delete { get; set; }
        public double Money { get; set; }
        public bool IsCheck { get; set; }
        public DateTime? SheetCheckTime { get; set; }

        [NotMapped]
        public string Address
        {
            get
            {
                var path = string.Empty;
                switch (SheetType)
                {
                    case SheetType.Daily:
                        path = "/Expense/Daily/Detail?sheetId=" + SheetId;
                        break;
                    case SheetType.Evection:
                        path = "/Expense/Evection/Detail?sheetId=" + SheetId;
                        break;
                    case SheetType.Reception:
                        path = "/expense/Reception/Detail?sheetId=" + SheetId;
                        break;
                }
                return path;
            }
        }
    }


}
