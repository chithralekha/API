namespace Magpie.Mapping
{
    public static class UserTaskMapper
    {
        public static Model.UserTask TranslateDTOTaskToModelUserTask(DTO.Task t)
        {

            if (t == null)
                return null;

            return new Model.UserTask
            {
                Code = t.Code,
                Comments = Mapper.TranslateDTOCommentListToModelCommentList(t.Comments),
                Completed = t.Completed,
                Control = ControlMapper.TranslateDTOControlToModelControl(t.Control),
                ControlId = t.ControlId,
                ControlCode = t.ControlCode,
                ControlSetCode = t.ControlSetCode,
                ControlSetId = t.ControlSetId,
                ControlSetTitle = t.ControlSetTitle,
                ControlTitle = t.ControlTitle,
                Created = t.Created,
                CreatedByUserId = t.CreatedByUserId,
                Description = t.Description,
                Due = t.Due,
                DueStatus = Mapper.TranslateDTODueStatusToModelDueStatus(t.DueStatus),
                Events = Mapper.TranslateDTOEventListToModelEventList(t.Events),
                Id = t.Id,
                Link = t.Link,
                RaciTeam = Mapper.TranslateDTORaciTeamToModelRaciTeam(t.RaciTeam),
                TaskDefinitionId = t.TaskDefinitionId,
                TaskState = Mapper.TranslateDTOTaskStateToModelTaskState(t.TaskState),
                Title = t.Title,
                WorkingSet = WorkingSetMapper.TranslateDTOWorkingSetToModelWorkingSet(t.WorkingSet),
                WorkingSetId = t.WorkingSetId
            };
        }

        public static Model.UserTask TranslateDTOTaskInfoToModelUserTask(DTO.TaskInfo ti)
        {

            throw new System.NotImplementedException();

            if (ti == null)
                return null;

            return new Model.UserTask
            {
                Code = ti.Code,
                //Comments = TranslateDTOCommentListToModelCommentList(ti.Comments),
                //Completed = ti.Completed,
                //Control = TranslateDTOControlToModelControl(ti.Control),
                //ControlId = ti.ControlId,
                //ControlSetId = ti.ControlSetId,
                //Created = ti.Created,
                //CreatedByUserId = ti.CreatedByUserId,
                //Description = ti.Description,
                //Due = ti.Due,
                //DueStatus = TranslateDTODueStatusToModelDueStatus(ti.DueStatus),
                //Events = TranslateDTOEventListToModelEventList(ti.Events),
                //Id = ti.Id,
                //Link = ti.Link,
                //RaciTeam = TranslateDTORaciTeamToModelRaciTeam(ti.RaciTeam),
                //ResponsibleUserId = ti.ResponsibleUserId,
                //TaskDefinitionId = ti.TaskDefinitionId,
                //TaskState = TranslateDTOTaskStateToModelTaskState(ti.TaskState),
                //Title = ti.Title,
                //WorkingSet = TranslateDTOWorkingSetToModelWorkingSet(ti.WorkingSet),
                //WorkingSetId = ti.WorkingSetId
            };
        }

        public static DTO.Task TranslateModelUserTaskToDTOTask(Model.UserTask ut)
        {
            if (ut == null)
                return null;

            return new DTO.Task
            {
                Code = ut.Code,
                Comments = Mapper.TranslateModelCommentListToDTOCommentList(ut.Comments),
                Completed = ut.Completed,
                Control = ControlMapper.TranslateModelControlToDTOControl(ut.Control),
                ControlId = ut.ControlId,
                ControlCode = ut.ControlCode,
                ControlSetCode = ut.ControlSetCode,
                ControlSetId = ut.ControlSetId,
                ControlSetTitle = ut.ControlSetTitle,
                ControlTitle = ut.ControlTitle,
                Created = ut.Created,
                CreatedByUserId = ut.CreatedByUserId,
                Description = ut.Description,
                Due = ut.Due,
                DueStatus = Mapper.TranslateModelDueStatusToDTODueStatus(ut.DueStatus),
                Events = Mapper.TranslateModelEventListToDTOEventList(ut.Events),
                Id = ut.Id,
                Link = ut.Link,
                RaciTeam = Mapper.TranslateModelRaciTeamToDTORaciTeam(ut.RaciTeam),
                TaskDefinitionId = ut.TaskDefinitionId,
                TaskState = Mapper.TranslateModelTaskStateToDTOTaskState(ut.TaskState),
                Title = ut.Title,
                WorkingSet = WorkingSetMapper.TranslateModelWorkingSetToDTOWorkingSet(ut.WorkingSet),
                WorkingSetId = ut.WorkingSetId

            };
        }

        public static DTO.TaskInfo TranslateModelUserTaskToDTOTaskInfo(Model.UserTask ut)
        {
            if (ut == null)
                return null;

            return new DTO.TaskInfo
            {
                Id = ut.Id,
                Code = ut.Code,
                Source = (ut.Control != null && ut.Control.DefinitionSource != null) ? ut.Control.DefinitionSource.Source : null,
                ControlId = ut.ControlId,
                ControlCode = ut.ControlCode,
                ControlSetCode = ut.ControlSetCode,
                ControlSetId = ut.ControlSetId,
                ControlSetTitle = ut.ControlSetTitle,
                ControlTitle = ut.ControlTitle,
                TaskState = Mapper.TranslateModelTaskStateToDTOTaskState(ut.TaskState),
                ResponsibleUser = (ut.RaciTeam != null) ? UserMapper.TranslateModelUserToDTOUser(ut.RaciTeam.ResponsibleUser) : null,
                Due = ut.Due,
                DueStatus = Mapper.TranslateModelDueStatusToDTODueStatus(ut.DueStatus),
                Completed = ut.Completed,
                Title = ut.Title
            };
        }
    }
}
