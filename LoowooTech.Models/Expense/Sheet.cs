using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("sheet")]
    public class Sheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public double Money { get; set; }
        public string Remark { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
