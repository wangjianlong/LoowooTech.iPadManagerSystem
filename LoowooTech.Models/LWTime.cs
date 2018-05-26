  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    public class LWTime
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Days { get; set; }


        public string LabelString
        {
            get
            {
                return string.Format("{0}年{1}月{2}日", Year, Month, Days);
            }
        }

        public string LabelData
        {
            get { return string.Format("{0}-{1}-{2}", Year, Month, Days); }
        }
    }
}
