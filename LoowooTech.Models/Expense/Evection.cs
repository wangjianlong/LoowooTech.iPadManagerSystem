using LoowooTech.Common;
using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("evection")]
    public class Evection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("CityIds")]
        public string CityIdsValues { get; set; }
        [NotMapped]
        public int[] CityIds
        {
            get
            {
                if (string.IsNullOrEmpty(CityIdsValues)) return null;
                return CityIdsValues.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    CityIdsValues = null;
                }
                else
                {
                    CityIdsValues = string.Join(",", value);
                }
            }
        }
        [NotMapped]
        public List<City> Citys { get; set; }
        public double Hotel { get; set; }
        public string Other { get; set; }
        public double Fee { get; set; }
        public int SheetId { get; set; }
        public virtual List<Errand> Errands { get; set; }
        public virtual List<Traffic> Traffics { get; set; }
        public double Sum
        {
            get
            {
                return Hotel + Fee + (Errands != null ? Errands.Sum(e => e.Fee) : .0) + (Traffics != null ? Traffics.Where(e => e.Vehicles != Vehicles.InternetCar).Sum(e => e.Cost) : .0);
            }
        }
    }
}
