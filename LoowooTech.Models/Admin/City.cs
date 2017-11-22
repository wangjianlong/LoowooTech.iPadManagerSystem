using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models.Admin
{
    [Table("city")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Delete { get; set; }
        public int? ParentId { get; set; }
        public string Remark { get; set; }
        [NotMapped]
        public List<City> Children { get; set; }
        [NotMapped]
        public string ParentName { get; set; }
    }
}
