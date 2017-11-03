using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("LWSystem")]
    public class LWSystem
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
        public int Order { get; set; }
        public bool Delete { get; set; }
        public DateTime Time { get; set; }
    }
}
