using LoowooTech.Common;
using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("reception")]
    public class Reception
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        [Column("CompanyIds")]
        public string CompanyIdValues { get; set; }
        [NotMapped]
        public int[] CompanyIds
        {
            get
            {
                if (string.IsNullOrEmpty(CompanyIdValues)) return null;
                return CompanyIdValues.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    CompanyIdValues = null;
                }
                else
                {
                    CompanyIdValues = string.Join(",", value);
                }
            }
        }
        [NotMapped]
        public List<Company> Companys { get; set; }
        public int Number { get; set; }
        [Column("userIds")]
        public string UserIdValues { get; set; }
        [NotMapped]
        public int[] UserIds
        {
            get
            {
                if (string.IsNullOrEmpty(UserIdValues)) return null;
                return UserIdValues.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    UserIdValues = null;
                }
                else
                {
                    UserIdValues = string.Join(",", value);
                }
            }
        }
        [NotMapped]
        public List<User> Users { get; set; }
        public string Remark { get; set; }
        public int SheetId { get; set; }
        public virtual Sheet Sheet { get; set; }
        public virtual List<ReceptionItem> Items { get; set; }
    }


    [Table("reception_item")]
    public class ReceptionItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Content { get; set; }
        public double Cost { get; set; }
        public PayWay PayWay { get; set; }
        public string Memo { get; set; }
        public int ReceptionId { get; set; }

        public static List<ReceptionItem> Generate(int receptionId,string[] contents,double[] costs,PayWay[] ways,string[] memos)
        {
            if (contents == null || costs == null || ways == null || memos == null || contents.Length != costs.Length || costs.Length != ways.Length || ways.Length != memos.Length)
            {
                return null;
            }
            var list = new List<ReceptionItem>();
            for(var i = 0; i < contents.Length; i++)
            {
                list.Add(new ReceptionItem
                {
                    Content = contents[i],
                    Cost = costs[i],
                    PayWay = ways[i],
                    Memo = memos[i],
                    ReceptionId = receptionId
                });
            }
            return list;
        }
      
    }

    public enum PayWay
    {
        [Description("现金")]
        Cash,
        [Description("支付宝")]
        AliPay,
        [Description("微信")]
        WeChat,
        [Description("Apple Pay等闪付")]
        ApplePay,
        [Description("信用卡/借记卡")]
        CreditCard,
        [Description("储值卡支付")]
        Card,
        [Description("挂账")]
        Bill,
        [Description("自备")]
        Self

    }
}
