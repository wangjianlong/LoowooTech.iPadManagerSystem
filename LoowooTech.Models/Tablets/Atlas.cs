using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    [Table("atlas")]
    public class Atlas:LWFather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Number { get; set; }
        public DateTime PublishTime { get; set; }
        public string Name { get; set; }
        public bool Delete { get; set; }
        public string Remark { get; set; }
        public int AccountId { get; set; }
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual FileInfo FileInfo { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
