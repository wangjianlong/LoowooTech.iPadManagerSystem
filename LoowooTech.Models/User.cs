using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models
{
    [Table("users")]
    public class User
    {
        public User()
        {
            RegisterTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime RegisterTime { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
        public bool Delete { get; set; }
        public string iPhone { get; set; }
        public virtual List<UserCompany> Companys { get; set; }
        public string LogoPath { get; set; }
        [NotMapped]
        public string AccessToken { get; set; }

    }
}
