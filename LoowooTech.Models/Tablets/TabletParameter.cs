using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    public class TabletParameter:ParameterBase
    {
        public string SerialNumer { get; set; }
        public int? TypeId { get; set; }
        public bool? Delete { get; set; }
        public int? BuyerId { get; set; }
        public int? AccountId { get; set; }
    }
}
