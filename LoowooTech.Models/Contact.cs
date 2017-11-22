using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("contact")]
    public class Contact
    {
        public Contact()
        {
            Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string Section { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string iPhone { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        public string EMail { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public string Company { get; set; }
        public string Remark { get; set; }
        public string Logo { get; set; }
        public DateTime Time { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
    }
}
