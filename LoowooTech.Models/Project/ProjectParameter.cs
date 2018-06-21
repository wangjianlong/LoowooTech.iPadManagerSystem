using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Project
{
    public class ProjectParameter:ParameterBase
    {
        public bool? IsDone { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? UserId { get; set; }
        public bool? Delete { get; set; }

        public int? CompanyAId { get; set; }
        public int? CompanyBId { get; set; }
        public int? ProjectTypeId { get; set; }
        public int? CityId { get; set; }
        public int? Year { get; set; }
    }
}
