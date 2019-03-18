using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    [Table("tablets")]
    public class Tablet:LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        public string Title
        {
            get
            {
                return string.Format("T{0}{1}{2}{3}", Time.Year, Time.Month.ToString("00"), Time.Day.ToString("00"), ID.ToString("0000"));
            }
        }
        /// <summary>
        /// 购买人
        /// </summary>
        public int? BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual User Buyer { get; set; }
        public string BuyerName
        {
            get
            {
                return Buyer != null ? Buyer.Name : "不详";
            }
        }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime? BuyTime { get; set; }
        public bool Delete { get; set; }

        /// <summary>
        /// 平板类型
        /// </summary>
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual TabletType Type { get; set; }

        /// <summary>
        /// 录入人员
        /// </summary>
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }



        /// <summary>
        /// 开发者账号
        /// </summary>
        public int? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string AccountName
        {
            get
            {
                return Account != null ? Account.Name : "不详";
            }
        }
        public string Code { get; set; }
        public string Remark { get; set; }

        public virtual  List<TabletRecord> TabletRecords { get; set; }
        public bool CanUse
        {
            get
            {
                if (TabletRecords == null)
                {
                    return true;
                }
                return TabletRecords.Where(e => e.Record.Delete == false && e.RevertId == null).Count() > 0 ? false : true;
            }
        }
    }
}
