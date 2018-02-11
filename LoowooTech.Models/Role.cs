using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }


    public enum UserRole
    {
        [Description("游客")]
        Guest,
        [Description("一般用户")]
        Common,
        [Description("高级用户")]
        Advance,
        [Description("管理员")]
        Admin,
    }

    public enum LWOrder
    {
        [Description("ID升序")]
        ID,
        [Description("ID降序")]
        DeID,
        [Description("时间升序")]
        Time,
        [Description("时间降序")]
        DeTime
    }
}
