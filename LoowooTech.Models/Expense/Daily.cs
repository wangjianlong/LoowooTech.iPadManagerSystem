using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("daily")]
    public class Daily
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int SheetId { get; set; }
        [ForeignKey("SheetId")]
        public virtual Sheet Sheet { get; set; }
        public virtual List<Substance> Substances { get; set; }
        public double Sum
        {
            get
            {
                return Substances == null ? .0 : Substances.Sum(e => e.Cost);
            }
        }
    }
}
