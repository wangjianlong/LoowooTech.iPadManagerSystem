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
        [NotMapped]
        public City Parent { get; set; }
        public string Remark { get; set; }
        [NotMapped]
        public List<City> Children { get; set; }
        [NotMapped]
        public string ParentName { get; set; }

        [NotMapped]
        public string LongName
        {
            get
            {
                var prev = GetParentName();
                if (prev != null)
                {
                    return Name + "-" + prev;
                }
                return Name;
            }
        }
        
        public string GetParentName()
        {
            if (Parent == null)
            {
                return null;
            }
            var prev= Parent.GetParentName();
            if (prev != null)
            {
                return Parent.Name + "-" + prev;
            }
            return Parent.Name;
        }
    }
}
