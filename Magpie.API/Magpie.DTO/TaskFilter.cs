using System;
using System.Collections.Generic;

namespace Magpie.DTO
{
    public class TaskFilter
    {
        #region Metadata

        public int FilterId { get; set; }
        public string FilterName { get; set; }
        public string FilterOwnerUserId { get; set; }
        public int FilterTypeId { get; set; }
        public string FilterType { get; set; }

        #endregion

        public int? AssignedStatusId { get; set; }
        public int? ControlId { get; set; }
        public int? ControlSetId { get; set; }
        public DateTime? DueEndDate { get; set; }
        public DateTime? DueStartDate { get; set; }
        public int? DueStatusId { get; set; }
        public bool? IncludeRelations { get; set; }
        public string ResponsibleUserId { get; set; }
        public int? TaskStateId { get; set; }
        public string UserTaskCode { get; set; }
        public int? UserTaskId { get; set; }
        public int? WorkingSetId { get; set; }

        public IEnumerable<DTO.TaskFilterResultCount> TaskFilterResultCounts { get; set; }
    }
}