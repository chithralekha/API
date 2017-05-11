using System.Collections.Generic;

namespace Magpie.Mapping
{
    public static class ControlMapper
    {
        public static Model.Control TranslateDTOControlToModelControl(DTO.Control c)
        {
            if (c == null)
                return null;

            return new Model.Control
            {
                Code = c.Code,
                ControlPriority = ControlPriorityMapper.TranslateDTOControlPriorityToModelControlPriority(c.ControlPriority),
                DefinitionSource = DefinitionSourceMapper.TranslateDTODefinitionSourceToModelDefinitionSource(c.DefinitionSource),
                Id = c.Id,
                MagpieCoreControlGuid = c.MagpieCoreControlGuid,
                Title = c.Title,
                Weight = c.Weight
            };
        }

        public static DTO.Control TranslateModelControlToDTOControl(Model.Control c)
        {
            if (c == null)
                return null;

            return new DTO.Control
            {
                Code = c.Code,
                ControlPriority = ControlPriorityMapper.TranslateModelControlPriorityToDTOControlPriority(c.ControlPriority),
                DefinitionSource = DefinitionSourceMapper.TranslateModelDefinitionSourceToDTODefinitionSource(c.DefinitionSource),
                Id = c.Id,
                MagpieCoreControlGuid = c.MagpieCoreControlGuid,
                Title = c.Title,
                Weight = c.Weight
            };
        }

        public static IEnumerable<Model.Control> TranslateDTOControlListToModelControlList(IEnumerable<DTO.Control> Controls)
        {
            if (Controls == null)
                return null;

            var modelControls = new List<Model.Control>();

            foreach (var cs in Controls)
            {
                modelControls.Add(TranslateDTOControlToModelControl(cs));
            }

            return modelControls;
        }

        public static IEnumerable<DTO.Control> TranslateModelControlListToDTOControlList(IEnumerable<Model.Control> Controls)
        {
            if (Controls == null)
                return null;

            var dtoControls = new List<DTO.Control>();

            foreach (var cs in Controls)
            {
                dtoControls.Add(TranslateModelControlToDTOControl(cs));
            }

            return dtoControls;
        }
    }
}
