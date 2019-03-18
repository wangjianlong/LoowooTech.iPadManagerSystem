﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Models
{
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
        [Column(TypeName = "int")]
        public iPadType Type { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        [Column(TypeName = "int")]
        public Space Space { get; set; }
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
        [Column(TypeName = "int")]
        public InternetWay Way { get; set; }
        /// <summary>
        /// 平板状态  空置  借用  项目
        /// </summary>
        [Column(TypeName = "int")]
        public iPadStatue Statue { get; set; }
        /// <summary>
        /// 信息录入者
        /// </summary>
        public string Enter { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Column(TypeName = "int")]
        public iPadColor Color { get; set; }
        [NotMapped]
        public List<Register_iPad> Relations { get; set; }
        [NotMapped]
        public bool HasInvoice
        {
            get
            {
                if (Relations == null)
                {
                    return false;
                }
                return Relations.FirstOrDefault(e => e.Relation == Relation.Invoice_iPad) != null;
            }
        }
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
}