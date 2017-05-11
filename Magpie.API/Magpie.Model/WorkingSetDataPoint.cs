using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Model
{
    public class WorkingSetDataPoint
    {
        public int WorkingSetId { get; set; }
        public int ControlSetId { get; set; }
        public DateTime Timestamp { get; set; }
        public int TotalTasks { get; set; }
        public int TotalNew { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalInJeopardy { get; set; }
        public int TotalOverdue { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalOnTime { get; set; }
        public int CompliancePercent { get; set; }
    }
}
