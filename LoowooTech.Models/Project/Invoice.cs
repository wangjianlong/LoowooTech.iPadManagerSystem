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
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string Img { get; set; }
        public bool Delete { get; set; }
        public InvoiceState State { get; set; }
        public InvoiceType Type { get; set; }
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
