using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Voucher
{
    [Table("EInvoices")]
    public class EInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public virtual User User { get; set; }
        /// <summary>
        /// 发票内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发票代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public int FileId { get; set; }
        public virtual FileInfo FileInfo { get; set; }

    }
}
