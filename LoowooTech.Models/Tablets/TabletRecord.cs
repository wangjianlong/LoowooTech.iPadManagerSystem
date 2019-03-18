using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    [Table("tablet_records")]
    public class TabletRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Tablet")]
        public int TabletId { get; set; }
    
        public virtual Tablet Tablet { get; set; }
        [ForeignKey("Record")]
        public int RecordId { get; set; }
   
        public virtual Record Record { get; set; }
        public string Remark { get; set; }
        [ForeignKey("Revert")]
        public int? RevertId { get; set; }
  
        public virtual Revert Revert { get; set; }

    
    }
}
