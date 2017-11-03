using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("users")]
    public class User
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime RegisterTime { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
        [NotMapped]
        public string AccessToken { get; set; }

    }
}
