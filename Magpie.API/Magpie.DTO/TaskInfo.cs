using System;

namespace Magpie.DTO
{
    public class TaskInfo
    {
        public string Code { get; set; }
        public DateTime? Completed { get; set; }
        public string ControlSetCode { get; set; }
        public int ControlSetId { get; set; }
        public string ControlSetTitle { get; set; }
        public string ControlCode { get; set; }
        public int ControlId { get; set; }
        public string ControlTitle { get; set; }
        public DateTime? Due { get; set; }
        public DueStatus DueStatus { get; set; }
        public int Id { get; set; }
        public string Source { get; set; }
        public User ResponsibleUser { get; set; }
        public string Title { get; set; }
        public TaskState TaskState { get; set; }
    }
}