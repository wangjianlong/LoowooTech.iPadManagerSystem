using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("traffic")]
    public class Traffic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }

    }

    public enum TrafficItem
    {
        [Description("公里")]
        KiloMeter,
        [Description("过路费")]
        Toll,
        [Description("油费")]
        CarPetty,
        [Description("车牌")]
        Plate
    }
}
