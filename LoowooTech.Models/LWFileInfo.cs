using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    public class LWFileInfo
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string FileName { get; set; }
        public string Ext { get; set; }
        
        public string Folder { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool Delete { get; set; }
    }
}
