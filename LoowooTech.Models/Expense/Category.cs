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
        public List<Category> Children { get; set; }
    }
}
