namespace Magpie.Mapping
{
    public static class ControlPriorityMapper
    {
        public static Model.ControlPriority TranslateDTOControlPriorityToModelControlPriority(DTO.ControlPriority cp)
        {
            if (cp == null)
                return null;

            return new Model.ControlPriority
            {
                Description = cp.Description,
                Id = cp.Id,
                Name = cp.Name
            };
        }

        public static DTO.ControlPriority TranslateModelControlPriorityToDTOControlPriority(Model.ControlPriority cp)
        {
            if (cp == null)
                return null;

            return new DTO.ControlPriority
            {
                Description = cp.Description,
                Id = cp.Id,
                Name = cp.Name
            };
        }
    }
}
