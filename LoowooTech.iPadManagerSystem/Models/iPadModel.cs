using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Models
{
    [Table("iPad_Model")]
    public class iPadModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ColorID { get; set; }
        public int SpaceID { get; set; }
        [ForeignKey("ColorID")]
        public virtual Color Color { get; set; }
        [ForeignKey("SpaceID")]
        public virtual iPadSpace Space { get; set; }
    }

    
}