using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
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
}
