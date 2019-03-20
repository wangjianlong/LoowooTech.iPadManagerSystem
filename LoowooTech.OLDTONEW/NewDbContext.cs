
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoowooTech.OLDTONEW
{
    public class NewDbContext:DbContext
    {
        public NewDbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<NewDbContext>(null);
        }

        public DbSet<User> users { get; set; }
        public DbSet<Tablet> Tablets { get; set; }
        public DbSet<TabletType> Tablet_Types { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<TabletRecord> TabletRecords { get; set; }
    }

    [Table("accounts")]
    public class Account : LWFather
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
        //public virtual List<Atlas> Atlas { get; set; }


    }

    [Table("tablets")]
    public class Tablet : LWFather
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

        public virtual List<TabletRecord> TabletRecords { get; set; }
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

    [Table("tablettypes")]
    public class TabletType : LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        /// <summary>
        /// 发布年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 屏幕尺寸
        /// </summary>
        public double Size { get; set; }
        /// <summary>
        /// 屏幕单元
        /// </summary>
        public string Unit { get; set; } = "英寸";
        /// <summary>
        /// 容量
        /// </summary>
        public int Space { get; set; }
        public string SpaceUnit { get; set; } = "GB";
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 网络连接方式
        /// </summary>
        public string InternetWay { get; set; }

        public string Remark { get; set; }
        public bool Delete { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string Title
        {
            get
            {
                return string.Format("{0} {1} {2}年{3}{4}({5}{6} {7} {8})", Company, Name, Year, Size, Unit, Space, SpaceUnit, InternetWay, Color);
            }
        }

    }
    public class LWFather
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        public bool IsNew
        {
            get
            {
                var sp = DateTime.Now - Time;
                return sp.TotalSeconds <= 172800;
            }
        }
    }

    [Table("tablet_records")]
    public class TabletRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Tablet")]
        public int TabletId { get; set; }

        public virtual Tablet Tablet { get; set; }
        [ForeignKey("Record")]
        public int RecordId { get; set; }

        public virtual Record Record { get; set; }
        public string Remark { get; set; }
        [ForeignKey("Revert")]
        public int? RevertId { get; set; }

        public virtual Revert Revert { get; set; }


    }
    [Table("reverts")]
    public class Revert : LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 归还事件
        /// </summary>
        public DateTime ReturnTime { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        /// <summary>
        /// 经办人/经办人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public virtual List<TabletRecord> TabletRecords { get; set; }
    }

    [Table("records")]
    public class Record : LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title
        {
            get
            {
                return string.Format("R{0}{1}{2}{3}", Time.Year, Time.Month.ToString("00"), Time.Day.ToString("00"), ID.ToString("0000"));
            }
        }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public int ProjectId { get; set; }

        public int CityId { get; set; }

        /// <summary>
        /// 用途/说明
        /// </summary>
        public string Purpose { get; set; }
        /// <summary>
        /// 使用单位
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime UseTime { get; set; }
        /// <summary>
        /// 领取人
        /// </summary>
        public string UseName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public bool Delete { get; set; }
        /// <summary>
        /// 录入人员
        /// </summary>
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //public int? FileId { get; set; }
        //[ForeignKey("FileId")]
        //public virtual FileInfo FileInfo { get; set; }
        public virtual List<TabletRecord> TabletRecords { get; set; }


    }


    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
