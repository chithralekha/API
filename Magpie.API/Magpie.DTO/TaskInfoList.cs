using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class TaskInfoList
    {
        public TaskInfoListMetadata Metadata { get; set; }
        public IEnumerable<DTO.TaskInfo> TaskInfos { get; set; }
    }
}
