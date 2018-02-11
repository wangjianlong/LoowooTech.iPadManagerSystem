using LoowooTech.Common;
using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("sheet_flow_view")]
    public class SheetFlowView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int FLowNodeId { get; set; }
        public string FlowNodeName { get; set; }
        public string UserIdValues { get; set; }
        [NotMapped]
        public int[] UserIds
        {
            get
            {
                if (string.IsNullOrEmpty(UserIdValues)) return null;
                return UserIdValues.ToIntArray();
            }
        }
        public int? UserId { get; set; }
        public VerificationState State { get; set; }
        public string Content { get; set; }
        public DateTime? CheckTime { get; set; }
        public bool Completed { get; set; }
        public FlowDataState FlowDataState { get; set; }
        public int SheetId { get; set; }
        public DateTime SheetTime { get; set; }
        [NotMapped]
        public string SerialNumber { get { return string.Format("E{0}{1}{2}{3}", SheetTime.Year, SheetTime.Month.ToString("00"), SheetTime.Day.ToString("00"), SheetId.ToString("0000")); } }
        public int Count { get; set; }
        public string Remark { get; set; }
        public int SheetUserId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SheetType SheetType { get; set; }
        public int ProjectId { get; set; }
        public bool Delete { get; set; }
        public double Money { get; set; }
    }

    public class SheetViewParameter:ParameterBase
    {
        /// <summary>
        /// 审核人员
        /// </summary>
        public int? CheckUserId { get; set; }
        /// <summary>
        /// 报销单报销人员
        /// </summary>
        public int? SheetUserId { get; set; }
        public int? FinialUserId { get; set; }
        /// <summary>
        /// 报销单位
        /// </summary>
        public int? CompanyId { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public VerificationState? State { get; set; }
        /// <summary>
        /// 审核环节
        /// </summary>
        public int? FLowNodeId { get; set; }
        public SheetType? SheetType { get; set; }
    }
}
