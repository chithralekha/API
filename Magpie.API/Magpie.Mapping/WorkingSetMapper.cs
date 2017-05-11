namespace Magpie.Mapping
{
    public static class WorkingSetMapper
    {
        public static Model.WorkingSet TranslateDTOWorkingSetToModelWorkingSet(DTO.WorkingSet ws)
        {
            if (ws == null)
                return null;

            return new Model.WorkingSet
            {
                City = ws.City,
                Deployed = ws.Deployed,
                DeployedByUserId = ws.DeployedByUserId,
                DeployedByUsername = ws.DeployedByUsername,
                Description = ws.Description,
                Name = ws.Name,
                StateId = ws.StateId,
                State = ws.State,
                WorkingSetGuid = ws.WorkingSetGuid,
                WorkingSetId = ws.WorkingSetId,
                WorkingSetTemplateId = ws.WorkingSetTemplateId,
                WorkingSetTemplate = WorkingSetTemplateMapper.TranslateDTOWorkingTemplateSetToModelWorkingSetTemplate(ws.WorkingSetTemplate),
                ZipCode = ws.ZipCode,
                Compliance = ws.Compliance,
                Users = UserMapper.TranslateDTOUserListToModelUserList(ws.Users)
            };
        }

        public static DTO.WorkingSet TranslateModelWorkingSetToDTOWorkingSet(Model.WorkingSet ws)
        {
            if (ws == null)
                return null;

            return new DTO.WorkingSet
            {
                City = ws.City,
                Deployed = ws.Deployed,
                DeployedByUserId = ws.DeployedByUserId,
                Description = ws.Description,
                Name = ws.Name,
                StateId = ws.StateId,
                WorkingSetGuid = ws.WorkingSetGuid,
                WorkingSetId = ws.WorkingSetId,
                WorkingSetTemplateId = ws.WorkingSetTemplateId,
                WorkingSetTemplate = WorkingSetTemplateMapper.TranslateModelWorkingSetTemplateToDTOWorkingSetTemplate(ws.WorkingSetTemplate),
                ZipCode = ws.ZipCode,
                Compliance = ws.Compliance,
                DataPoint = WorkingSetHistoryMapper.TranslateModelWorkingSetHistoryToDTOWorkingSet(ws.WorkingSetDataPoint),
                Users = UserMapper.TranslateModelUserListToDTOUserList(ws.Users)
            };
        }
    }
}
