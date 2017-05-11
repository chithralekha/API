

using System;

namespace Magpie.Mapping
{
    public class WorkingSetHistoryMapper
    {
        public static Model.WorkingSetDataPoint TranslateDTOWorkingSetHistoryToModelWorkingSet(DTO.WorkingSetDataPoint dp)
        {
            if (dp == null)
                return null;

            return new Model.WorkingSetDataPoint
            {
                ControlSetId = dp.ControlSetId,
                WorkingSetId = dp.WorkingSetId,
                Timestamp = dp.Timestamp,
                TotalTasks = dp.TotalTasks,
                TotalNew = dp.TotalNew,
                TotalInProgress = dp.TotalInProgress,
                TotalInJeopardy = dp.TotalInJeopardy,
                TotalOverdue = dp.TotalOverdue,
                TotalCompleted = dp.TotalCompleted,
                CompliancePercent = dp.CompliancePercent,
                TotalOnTime = dp.TotalOnTime
              };
        }

        public static DTO.WorkingSetDataPoint TranslateModelWorkingSetHistoryToDTOWorkingSet(Model.WorkingSetDataPoint dp)
        {
            if (dp == null)
                return null;

            return new DTO.WorkingSetDataPoint
            {
                ControlSetId = dp.ControlSetId,
                WorkingSetId = dp.WorkingSetId,
                Timestamp = (DateTime.MinValue == dp.Timestamp)? DateTime.Now : dp.Timestamp,
                TotalTasks = dp.TotalTasks,
                TotalNew = dp.TotalNew,
                TotalInProgress = dp.TotalInProgress,
                TotalInJeopardy = dp.TotalInJeopardy,
                TotalOverdue = dp.TotalOverdue,
                TotalCompleted = dp.TotalCompleted,
                CompliancePercent = dp.CompliancePercent,
                TotalOnTime = dp.TotalOnTime
            };
        }      
    }
}
