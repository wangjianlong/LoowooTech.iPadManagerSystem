using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Models.Expense
{
    [Table("category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Delete { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public Category Prev { get; set; }
        [NotMapped]
        public List<Category> Children { get; set; }
        [NotMapped]
        public string PrevName { get; set; }
        [NotMapped]
        public string LongName
        {
            get
            {
                var prev = GetPrevName();
                if (prev != null)
                {
                    return Name + "-" + prev;
                }
                return Name;
            }
        }

        public string GetPrevName()
        {
            if (Prev == null)
            {
                return null;
            }
            var prev = Prev.GetPrevName();
            if (prev != null)
            {
                return Prev.Name + "-" + prev;
            }
            return Prev.Name;
        }
    }
}
