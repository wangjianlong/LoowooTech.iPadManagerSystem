﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    /// <summary>
    /// 合同付款方式
    /// </summary>
    [Table("payinfo")]
    public class PayInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        /// <summary>
        /// 付款信息
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 金额  单元：元
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractId { get; set; }


        public static List<PayInfo> Tranlate(int contractId,string[] contents,double[] moneys)
        {
            var list = new List<PayInfo>();
            for(var i = 0; i < contents.Length; i++)
            {
                if (moneys.Length > i)
                {
                    list.Add(new PayInfo
                    {
                        Content = contents[i],
                        ContractId = contractId,
                        Money = moneys[i]
                    });
                }
            }

            return list;
        }

    }
}
