using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    /// <summary>
    /// 开发者账号
    /// </summary>
    [Table("accounts")]
    public class Account:LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string TelPhone { get; set; }
        /// <summary>
        /// 账号注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<Atlas> Atlas { get; set; }


    }
}
