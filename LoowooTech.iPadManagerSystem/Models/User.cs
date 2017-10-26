using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PassWord { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool Approve { get; set; }

        [NotMapped]
        public string AccessToken { get; set; }
    }
}