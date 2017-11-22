using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("LWSystem")]
    public class LWSystem:Evaluate
    {
        public LWSystem()
        {
            Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string iConClass { get; set; }
        public string WidgetClass { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Delete { get; set; }
        public bool IsOnline { get; set; }
        public DateTime Time { get; set; }
        public virtual List<Power> Powers { get; set; }
    }
    public enum WidgetClass
    {
        [Description("navy-bg")]
        navybg,
        [Description("lazur-bg")]
        lazurbg,
        [Description("yellow-bg")]
        yellowbg,
        [Description("red-bg")]
        redbg
    }
}
