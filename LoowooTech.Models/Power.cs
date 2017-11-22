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
    [Table("power")]
    public class Power
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string iConClass { get; set; }
        public string Class { get; set; }
        public PowerEnum PowerEnum { get; set; }
        public PowerType PowerType { get; set; }
        public bool Delete { get; set; }
        public string Remark { get; set; }
        public int LWSystemId { get; set; }
        //public virtual LWSystem LWSystem { get; set; }

        public virtual List<PowerItem> Items { get; set; }
    }

    public enum PowerEnum
    {
        [Description("按钮")]
        Button,
        [Description("链接")]
        Link,
        [Description("视图")]
        View
    }

    public enum PowerType
    {
        [Description("弹窗")]
        Windows,
        [Description("地址")]
        Address
    }
    
}
