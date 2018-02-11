using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    public class ProjectProgress
    {
        public int ID { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime Time { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Content { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProjectId { get; set; }
    }
}
