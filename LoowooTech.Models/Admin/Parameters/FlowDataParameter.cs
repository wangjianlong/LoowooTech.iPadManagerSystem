using LoowooTech.Models.Admin;
using System;

namespace LoowooTech.Models
{
    public class FlowDataParameter:ParameterBase
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? FlowId { get; set; }
        public int? UserId { get; set; }
        public bool? Completed { get; set; }
        public VerificationState? State { get; set; }
    }
}
