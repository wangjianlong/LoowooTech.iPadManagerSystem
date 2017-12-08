using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("substance")]
    public class Substance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public double Cost { get; set; }
        public string Content { get; set; }
        public int DailyId { get; set; }
        public static List<Substance> Generate(int dailyId, int[] CategoryId,double[] cost,string[] content)
        {
            if (CategoryId == null || cost == null || content == null || CategoryId.Length != cost.Length || cost.Length != content.Length)
            {
                return null;
            }

            var list = new List<Substance>();
            for(var i = 0; i < CategoryId.Length; i++)
            {
                var entry = new Substance
                {
                    CategoryId = CategoryId[i],
                    Cost = cost[i],
                    Content = content[i],
                    DailyId = dailyId
                };
                list.Add(entry);
            }
            return list;
        }
    }
}
