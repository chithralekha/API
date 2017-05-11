using System;
using System.Collections.Generic;

namespace Magpie.DTO
{
    public class Task
    {
        //public string Code { get; set; }
        //public IEnumerable<Comment> Comments { get; set; }
        //public DateTime? Completed { get; set; }
        //public Control Control { get; set; }
        //public int ControlId { get; set; }
        //public ControlSet ControlSet { get; set; }
        //public int ControlSetId { get; set; }
        //public DateTime? Created { get; set; }
        //public string CreatedByUserId { get; set; }
        //public string Description { get; set; }
        //public DateTime? Due { get; set; }
        //public DueStatus DueStatus { get; set; }
        //public IEnumerable<Event> Events { get; set; }
        //public int Id { get; set; }
        //public string Link { get; set; }
        //public RaciTeam RaciTeam { get; set; }
        //public string ResponsibleUserId { get; set; }
        //public int? TaskDefinitionId { get; set; }
        //public TaskState TaskState { get; set; }
        //public string Title { get; set; }
        //public WorkingSet WorkingSet { get; set; }
        //public int WorkingSetId { get; set; }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public ControlSet ControlSet { get; set; }
        public Control Control { get; set; }
        public WorkingSet WorkingSet { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedByUserId { get; set; }
        public TaskState TaskState { get; set; }
        public string Description { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? Completed { get; set; }
        public string Link { get; set; }
        public DueStatus DueStatus { get; set; }
        public RaciTeam RaciTeam { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public int ControlSetId { get; set; }
        public string ControlSetCode { get; set; }
        public string ControlSetTitle { get; set; }
        public int ControlId { get; set; }
        public string ControlCode { get; set; }
        public string ControlTitle { get; set; }
        public int? RaciTeamId { get; set; }
        public int? TaskDefinitionId { get; set; }
        public int WorkingSetId { get; set; }
    }
}
