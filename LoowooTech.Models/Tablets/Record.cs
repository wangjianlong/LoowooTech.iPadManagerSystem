using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    [Table("records")]
    public class Record:LWFather
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
        [ForeignKey("ProjectId")]
        public virtual Project.Project Project { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
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

        public int? FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual FileInfo FileInfo { get; set; }
        public virtual List<TabletRecord> TabletRecords { get; set; }

        //public List<TabletRecord> Leaves
        //{
        //    get
        //    {
        //        return TabletRecords == null ? null : TabletRecords.Where(e => e.Tablet.Delete == false && e.RevertId == null).ToList();
        //    }
        //}


        //public bool CanEdit
        //{
        //    get
        //    {
        //        //if (Leaves == null)
        //        //{
        //        //    return true;
        //        //}
        //        //if (TabletRecords.Count == Leaves.Count)
        //        //{
        //        //    return Leaves.Count == 0;
        //        //}
        //        //return false;

        //        return Leaves == null ? true : (TabletRecords.Count == Leaves.Count ? Leaves.Count == 0 : false);

        //    }
        //}
    }
}
