using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class WorkingSet
    {
        public int WorkingSetId { get; set; }
        public Guid WorkingSetGuid { get; set; }
        public int WorkingSetTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeployedByUserId { get; set; }
        public DateTime Deployed { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public int? Compliance { get; set; }
        public string DeployedByUsername { get; set; }
        public string State { get; set; }
        public WorkingSetTemplate WorkingSetTemplate { get; set; }
        public WorkingSetDataPoint DataPoint { get; set; }
        public IEnumerable<User> Users { get; set; }

    }
}
