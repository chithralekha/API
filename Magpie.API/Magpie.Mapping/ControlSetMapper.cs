using System.Collections.Generic;

namespace Magpie.Mapping
{
    public static class ControlSetMapper
    {
        public static Model.ControlSet TranslateDTOControlSetToModelControlSet(DTO.ControlSet cs)
        {
            if (cs == null)
                return null;

            return new Model.ControlSet
            {
                Id = cs.Id,
                DefinitionSource = DefinitionSourceMapper.TranslateDTODefinitionSourceToModelDefinitionSource(cs.DefinitionSource),
                Code = cs.Code,
                Title = cs.Title,
                ControlSetClassification = ControlSetClassificationMapper.TranslateDTOControlSetClassificationToModelControlSetClassification(cs.ControlSetClassification),
                Controls = ControlMapper.TranslateDTOControlListToModelControlList(cs.Controls),
                MagpieCoreControlSetGuid = cs.MagpieCoreControlSetGuid,
                Version = cs.Version,
            };
        }

        public static DTO.ControlSet TranslateModelControlSetToDTOControlSet(Model.ControlSet cs)
        {
            if (cs == null)
                return null;

            return new DTO.ControlSet
            {
                Id = cs.Id,
                DefinitionSource = DefinitionSourceMapper.TranslateModelDefinitionSourceToDTODefinitionSource(cs.DefinitionSource),
                Code = cs.Code,
                Title = cs.Title,
                ControlSetClassification = ControlSetClassificationMapper.TranslateModelControlSetClassificationToDTOControlSetClassification(cs.ControlSetClassification),
                Controls = ControlMapper.TranslateModelControlListToDTOControlList(cs.Controls),
                MagpieCoreControlSetGuid = cs.MagpieCoreControlSetGuid,
                Version = cs.Version,
            };
        }

        public static IEnumerable<Model.ControlSet> TranslateDTOControlSetListToModelControlSetList(IEnumerable<DTO.ControlSet> ControlSets)
        {
            if (ControlSets == null)
                return null;

            var modelControlSets = new List<Model.ControlSet>();

            foreach (var cs in ControlSets)
            {
                modelControlSets.Add(TranslateDTOControlSetToModelControlSet(cs));
            }

            return modelControlSets;
        }

        public static IEnumerable<DTO.ControlSet> TranslateModelControlSetListToDTOControlSetList(IEnumerable<Model.ControlSet> ControlSets)
        {
            if (ControlSets == null)
                return null;

            var dtoControlSets = new List<DTO.ControlSet>();

            foreach (var cs in ControlSets)
            {
                dtoControlSets.Add(TranslateModelControlSetToDTOControlSet(cs));
            }

            return dtoControlSets;
        }
    }
}
