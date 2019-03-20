using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoowooTech.OLDTONEW
{
    public class OLDDbContext:DbContext
    {
        public OLDDbContext() : base("name=DbConnection-OLD")
        {
            Database.SetInitializer<OLDDbContext>(null);
        }


        public DbSet<iPad> iPads { get; set; }
        public DbSet<iPadRegister> Registers { get; set; }
        public DbSet<Register_iPad> Register_IPads { get; set; }
    }

    [Table("ipads")]
    public class iPad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 平板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 平板类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        [Column(TypeName = "int")]
        public int Space { get; set; }
        /// <summary>
        /// 购买者
        /// </summary>
        public string Buyer { get; set; }
        /// <summary>
        /// Apple ID
        /// </summary>
        public int AID { get; set; }
        [NotMapped]
        public iPadAccount Account { get; set; }
        //  public string Account { get; set; }
        /// <summary>
        /// 网络连接方式
        /// </summary>
 
        public int Way { get; set; }
        /// <summary>
        /// 平板状态  空置  借用  项目
        /// </summary>

        public iPadStatue Statue { get; set; }
        /// <summary>
        /// 信息录入者
        /// </summary>
        public string Enter { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>

        public int Color { get; set; }
        [NotMapped]
        public List<Register_iPad> Relations { get; set; }
        //[NotMapped]
        //public bool HasInvoice
        //{
        //    get
        //    {
        //        if (Relations == null)
        //        {
        //            return false;
        //        }
        //        return Relations.FirstOrDefault(e => e.Relation == Relation.Invoice_iPad) != null;
        //    }
        //}
        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 授权文件
        /// </summary>
        public string AuthFile { get; set; }
        /// <summary>
        /// 授权文件路径
        /// </summary>
        public string AuthPath { get; set; }
    }

    public enum iPadColor
    {
        银色, 金色, 深空灰, 玫瑰金, 亮黑色
    }

    public enum iPadStatue
    {
        [Description("闲置")]
        Vacant,
        [Description("外借")]
        Borrow,
        [Description("项目使用")]
        Deliver
    }


    /// <summary>
    /// 平板管理系统种类
    /// 作者：汪建龙
    /// 编写时间：2016年12月8日10:51:31
    /// </summary>
    public enum iPadCategory
    {
        [Description("平板")]
        iPad,
        [Description("")]
        Register,
        [Description("合同")]
        Contract,
        [Description("发票")]
        Invoice,
        [Description("账号")]
        Account,
        [Description("联系人")]
        Contact,
        [Description("平板数据")]
        Datum,
        [Description("系统配置")]
        Configuration
    }

    public enum InternetWay
    {
        WLAN,
        WLAN_Cellular
    }

    public enum iPadType
    {
        [Description("iPad Mini2")]
        Mini2,
        [Description("iPad Mini4")]
        Mini4,
        [Description("iPad Air")]
        Air,
        [Description("iPad Air2")]
        Air2,
        [Description("iPad Pro(9.7英寸)")]
        Pro9,
        [Description("iPad Pro(12.9英寸)")]
        Pro12,
        [Description("iPad(9.7英寸)")]
        iPad2017,
        [Description("iPad Pro (10.5英寸)")]
        Pro10,
        [Description("iPad Pro (11英寸)")]
        iPadPro11
    }

    public enum Space
    {
        [Description("16G")]
        Sixteen,
        [Description("32G")]
        ThirtyTwo,
        [Description("64G")]
        SixtyFour,
        [Description("128G")]
        OneHundredAndTwentyEight,
        [Description("256G")]
        TwoHundredAndFivetySix
    }

    public enum TodoFile
    {
        Contract,
        iPad_Contract
    }

    [Table("ipad_accounts")]
    public class iPadAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string EPassword { get; set; }
        public string Enter { get; set; }
        public DateTime Time { get; set; }


    }

    [Table("register_ipads")]
    public class Register_iPad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int RID { get; set; }
        public int IID { get; set; }
        /// <summary>
        /// 归还标志
        /// </summary>
        public int Revert { get; set; }
        public string Restorer { get; set; }
        public DateTime? Time { get; set; }
        public int Relation { get; set; }
        [NotMapped]
        public iPad iPad { get; set; }
    }

    public enum Relation
    {
        Register_iPad,
        Contract_iPad,
        Invoice_iPad
    }
    public enum iPadRevert
    {
        [Description("使用中")]
        Using,
        [Description("归还")]
        Back,
        [Description("项目")]
        Projection
    }


    [Table("ipad_registers")]
    public class iPadRegister
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        [NotMapped]
        public string Number
        {
            get
            {
                return string.Format("{0}{1}{2}{3}", Time.Year, Time.Month.ToString("00"), Time.Day.ToString("00"), ID.ToString("00000000"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 用途/说明
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 借用者
        /// </summary>
        public string Borrower { get; set; }
        /// <summary>
        /// 借用时间
        /// </summary>
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// 信息录入人
        /// </summary>
        public string Enter { get; set; }
        [NotMapped]
        public bool Revert
        {
            get
            {
                if (Register_iPads == null)
                {
                    return false;
                }
                return Register_iPads.Where(e => e.Revert == 1).Count() == 0;
            }
        }
        [NotMapped]
        public List<Register_iPad> Register_iPads { get; set; }
        [NotMapped]
        public List<iPad> iPads
        {
            get
            {
                if (Register_iPads == null)
                {
                    return null;
                }
                return Register_iPads.Select(e => e.iPad).ToList();
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
