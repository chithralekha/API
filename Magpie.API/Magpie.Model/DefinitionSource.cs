using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Model
{
    public class DefinitionSource
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public Guid? MagpieCoreDefinitionSourceGuid { get; set; }
        public string Source { get; set; }
    }
}
