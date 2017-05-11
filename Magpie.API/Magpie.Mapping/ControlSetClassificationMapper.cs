namespace Magpie.Mapping
{
    public static class ControlSetClassificationMapper
    {
        public static Model.ControlSetClassification TranslateDTOControlSetClassificationToModelControlSetClassification(DTO.ControlSetClassification csc)
        {
            if (csc == null)
                return null;

            return new Model.ControlSetClassification
            {
                Id = csc.Id,
                Name = csc.Name
            };
        }

        public static DTO.ControlSetClassification TranslateModelControlSetClassificationToDTOControlSetClassification(Model.ControlSetClassification csc)
        {
            if (csc == null)
                return null;

            return new DTO.ControlSetClassification
            {
                Id = csc.Id,
                Name = csc.Name
            };
        }
    }
}
