using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Versions
{
    [Table("versions")]
    public class Version:LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Number { get; set; }
        public string Remark { get; set; }
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public bool Delete { get; set; }
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual FileInfo FileInfo { get; set; }

    }
}
