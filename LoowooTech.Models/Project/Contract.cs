using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    /// <summary>
    /// 合同信息表
    /// </summary>
    [Table("contract")]
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;



        /// <summary>
        /// 甲方
        /// </summary>
        public int CompanyAId { get; set; }
        [ForeignKey("CompanyAId")]
        public virtual Company CompanyA { get; set; }
        /// <summary>
        /// 乙方
        /// </summary>
        public int CompanyBId { get; set; }
        [ForeignKey("CompanyBId")]
        public virtual Company CompanyB { get; set; }
        /// <summary>
        /// 合同金额 单位：元
        /// </summary>
        public double Money { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Number { get; set; }
        public string TimeRange { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }




        public int ProjectId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual List<PayInfo> PayInfos { get; set; }
        public virtual List<ContractFile> ContractFiles { get; set; }
    }
}
