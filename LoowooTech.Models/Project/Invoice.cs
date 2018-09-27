using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    /// <summary>
    /// 项目发票
    /// </summary>
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int FileInfoId { get; set; }
        public virtual FileInfo FileInfo { get; set; }
        public string Code { get; set; }
        public double Money { get; set; }
        public bool Delete { get; set; }
        public InvoiceState State { get; set; }
        public InvoiceType Type { get; set; }
        /// <summary>
        /// 发票关联的项目Id
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 发票关联的合同ID  可空
        /// </summary>
        public int? ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        public string Remark { get; set; }

    }

    public enum InvoiceState
    {
        [Description("有效")]
        Effective,
        [Description("退票")]
        TUI,
        [Description("红票")]
        HONG
    }

    public enum InvoiceType
    {
        [Description("报销发票")]
        Expense,
        [Description("合同发票")]
        Contract
    }
}
