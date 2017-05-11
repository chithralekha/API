using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Model
{
    public class Control
    {
        public string Code { get; set; }
        public ControlPriority ControlPriority { get; set; }
        public DefinitionSource DefinitionSource { get; set; }
        public int Id { get; set; }
        public Guid MagpieCoreControlGuid { get; set; }
        public string Title { get; set; }
        public int Weight { get; set; }
    }
}
