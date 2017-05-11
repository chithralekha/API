using System;
using System.Collections.Generic;

namespace Magpie.Model
{
    public class ControlSet
    {
        public string Code { get; set; }
        public IEnumerable<Control> Controls { get; set; }
        public ControlSetClassification ControlSetClassification { get; set; }
        public DefinitionSource DefinitionSource { get; set; }
        public int Id { get; set; }
        public Guid? MagpieCoreControlSetGuid { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
    }
}
