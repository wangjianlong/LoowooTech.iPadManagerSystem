using LoowooTech.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("errand")]
    public class Errand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ErrandType ErrandType { get; set; }
        public int EvectionId { get; set; }
        [NotMapped]
        public double Fee
        {
            get
            {

                return ((EndTime - StartTime).Days + 1) * Price;
            }

        }

        [NotMapped]
        public double Price
        {
            get
            {
                if (ErrandType == ErrandType.Locale)
                {
                    return 100;
                }
                return 80;
            }
        }
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendFormat("时间：{0}-{1}；差补金额：{2}元", StartTime.ToLongDateString(), EndTime.ToLongDateString(), Fee);
                return sb.ToString();
            }
        }
        public static List<Errand> Generate(int evectionId, int[] userId,DateTime[] startTime,DateTime[] endTime,ErrandType errandType)
        {
            if (userId == null || startTime == null || endTime == null || userId.Length != startTime.Length || startTime.Length != endTime.Length)
            {
                return null;
            }
            var list = new List<Errand>();
            for(var i = 0; i < userId.Length; i++)
            {
                if (userId[i] > 0)
                {
                    var entry = new Errand
                    {
                        UserId = userId[i],
                        StartTime = startTime[i],
                        EndTime = endTime[i],
                        EvectionId = evectionId,
                        ErrandType = errandType
                    };
                    list.Add(entry);
                }
               
            }
            return list;
        }
    }


    public enum ErrandType
    {
        [Description("普通出差")]
        Common,
        [Description("驻场外业")]
        Locale
    }
}
