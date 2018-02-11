using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models
{
    public class ParameterBase
    {
        public LWOrder Order { get; set; }
        public PageParameter Page { get; set; }
    }
}
