using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.DTO
{
    public class ControlPriority
    {
        #region Fields deliberately not exposed
        // DefinitionSource 
        #endregion

        public string Description { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
