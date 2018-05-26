using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Admin
{
    [Table("company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public string USCI { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string Account { get; set; }
        public WidgetClass? Widget { get; set; }
        public bool Delete { get; set; }
        /// <summary>
        /// 判断是否为乙方 单位  陆吾科技和天启为null
        /// </summary>
        public bool? IsProject { get; set; }
    }
}
