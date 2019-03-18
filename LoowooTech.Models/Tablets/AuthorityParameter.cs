using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Tablets
{
    public class AuthorityParameter:ParameterBase
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public bool? Delete { get; set; }
    }
}
