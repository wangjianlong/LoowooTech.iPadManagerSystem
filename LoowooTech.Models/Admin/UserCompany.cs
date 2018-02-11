using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Admin
{
    [Table("user_company")]
    public class UserCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
