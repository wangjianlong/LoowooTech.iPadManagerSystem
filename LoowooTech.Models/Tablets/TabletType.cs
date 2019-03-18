using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("tablettypes")]
    public class TabletType:LWFather
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
                return string.Format("{0} {1} {2}年{3}{4}({5}{6} {7} {8})", Company, Name, Year, Size, Unit, Space,SpaceUnit, InternetWay,Color);
            }
        }

    }
}
