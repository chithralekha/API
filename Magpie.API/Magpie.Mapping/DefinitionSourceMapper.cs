namespace Magpie.Mapping
{
    public static class DefinitionSourceMapper
    {
        public static Model.DefinitionSource TranslateDTODefinitionSourceToModelDefinitionSource(DTO.DefinitionSource ds)
        {
            if (ds == null)
                return null;

            return new Model.DefinitionSource
            {
                Code = ds.Code,
                Id = ds.Id,
                MagpieCoreDefinitionSourceGuid = ds.MagpieCoreDefinitionSourceGuid,
                Source = ds.Source
            };
        }

        public static DTO.DefinitionSource TranslateModelDefinitionSourceToDTODefinitionSource(Model.DefinitionSource ds)
        {
            if (ds == null)
                return null;

            return new DTO.DefinitionSource
            {
                Code = ds.Code,
                Id = ds.Id,
                MagpieCoreDefinitionSourceGuid = ds.MagpieCoreDefinitionSourceGuid,
                Source = ds.Source
            };
        }
    }
}
