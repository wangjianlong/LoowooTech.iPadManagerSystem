using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Versions
{
    public class ProgramParameter:ParameterBase
    {
        public string Name { get; set; }
        public int? UserId { get; set; }
        public bool? Delete { get; set; }
    }
}
