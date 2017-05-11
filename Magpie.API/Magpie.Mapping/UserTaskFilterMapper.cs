using System.Collections.Generic;

namespace Magpie.Mapping
{
    public static class UserTaskFilterMapper
    {
        public static Model.UserTaskFilter TranslateDTOTaskFilterToModelUserTaskFilter(DTO.TaskFilter tf)
        {
            if (tf == null)
                return null;

            return new Model.UserTaskFilter
            {
                AssignedStatusId = tf.AssignedStatusId,
                ControlId = tf.ControlId,
                ControlSetId = tf.ControlSetId,
                DueEndDate = tf.DueEndDate,
                DueStartDate = tf.DueStartDate,
                DueStatusId = tf.DueStatusId,
                IncludeRelations = tf.IncludeRelations,
                ResponsibleUserId = tf.ResponsibleUserId,
                TaskStateId = tf.TaskStateId,
                UserTaskCode = tf.UserTaskCode,
                UserTaskId = tf.UserTaskId,
                FilterId = tf.FilterId,
                FilterName = tf.FilterName,
                FilterOwnerUserId = tf.FilterOwnerUserId,
                FilterType = tf.FilterType,
                FilterTypeId = tf.FilterTypeId,
                WorkingSetId = tf.WorkingSetId,
                UserTaskFilterResultCounts = TranslateDTOTaskFilterResultCountListToModelUserTaskFilterResultCountList(tf.TaskFilterResultCounts)
            };
        }

        public static Model.UserTaskFilterResultCount TranslateDTOTaskFilterResultCountToModelUserTaskFilterResultCount(DTO.TaskFilterResultCount tfrc)
        {
            if (tfrc == null)
                return null;

            return new Model.UserTaskFilterResultCount
            {
                Count = tfrc.Count,
                //FilterId = tfrc.FilterId,
                WorkingSetId = tfrc.WorkingSetId
            };
        }

        public static IEnumerable<Model.UserTaskFilterResultCount> TranslateDTOTaskFilterResultCountListToModelUserTaskFilterResultCountList(IEnumerable<DTO.TaskFilterResultCount> userTaskFilterResultCounts)
        {
            if (userTaskFilterResultCounts == null)
                return null;

            var modelTaskFilterResultCounts = new List<Model.UserTaskFilterResultCount>();

            foreach (var item in userTaskFilterResultCounts)
            {
                modelTaskFilterResultCounts.Add(TranslateDTOTaskFilterResultCountToModelUserTaskFilterResultCount(item));
            }

            return modelTaskFilterResultCounts;
        }

        public static DTO.TaskFilter TranslateModelUserTaskFilterToDTOTaskFilter(Model.UserTaskFilter utf)
        {
            if (utf == null)
                return null;

            return new DTO.TaskFilter
            {
                AssignedStatusId = utf.AssignedStatusId,
                ControlId = utf.ControlId,
                ControlSetId = utf.ControlSetId,
                DueEndDate = utf.DueEndDate,
                DueStartDate = utf.DueStartDate,
                DueStatusId = utf.DueStatusId,
                IncludeRelations = utf.IncludeRelations,
                ResponsibleUserId = utf.ResponsibleUserId,
                TaskStateId = utf.TaskStateId,
                UserTaskCode = utf.UserTaskCode,
                UserTaskId = utf.UserTaskId,
                FilterId = utf.FilterId,
                FilterName = utf.FilterName,
                FilterOwnerUserId = utf.FilterOwnerUserId,
                FilterType = utf.FilterType,
                FilterTypeId = utf.FilterTypeId,
                WorkingSetId = utf.WorkingSetId,
                TaskFilterResultCounts = TranslateModelUserTaskFilterResultCountListToDTOTaskFilterResultCountList(utf.UserTaskFilterResultCounts)
            };
        }

        public static DTO.TaskFilterResultCount TranslateModelUserTaskFilterResultCountToDTOTaskFilterResultCount(Model.UserTaskFilterResultCount utfrc)
        {
            if (utfrc == null)
                return null;

            return new DTO.TaskFilterResultCount
            {
                Count = utfrc.Count,
                //FilterId = utfrc.FilterId,
                WorkingSetId = utfrc.WorkingSetId
            };
        }

        public static IEnumerable<DTO.TaskFilterResultCount> TranslateModelUserTaskFilterResultCountListToDTOTaskFilterResultCountList(IEnumerable<Model.UserTaskFilterResultCount> userTaskFilterResultCounts)
        {
            if (userTaskFilterResultCounts == null)
                return null;

            var dtoTaskFilterResultCounts = new List<DTO.TaskFilterResultCount>();

            foreach (var item in userTaskFilterResultCounts)
            {
                dtoTaskFilterResultCounts.Add(TranslateModelUserTaskFilterResultCountToDTOTaskFilterResultCount(item));
            }

            return dtoTaskFilterResultCounts;
        }
    }
}
