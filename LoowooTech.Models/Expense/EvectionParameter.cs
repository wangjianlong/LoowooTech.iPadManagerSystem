using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    public class EvectionParameter:SheetParameter
    {
        public int? CityId { get; set; }
        public double? MinHotel { get; set; }
        public double? MaxHotel { get; set; }
        public string Other { get; set; }
        public Vehicles? Vehicales { get; set; }
        public int? UserId2 { get; set; }
    }
}
