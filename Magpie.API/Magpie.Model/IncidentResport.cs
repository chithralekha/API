using System;
using System.Collections.Generic;

namespace Magpie.Model
{
    public partial class IncidentReport
    {
        public int IncidentType { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string ReporterName { get; set; }
        public DateTime? ReportDate { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string CompromisedInformation { get; set; }
        public string CompromisedSystems { get; set; }
        public string Location { get; set; }
        public string AffectedItems { get; set; }
        public string Damage { get; set; }
        public string Summary { get; set; }
        public string Agencies { get; set; }
        public string SupervisorUserId { get; set; }
        public string CompletedByUserId { get; set; }
        public string ReviewedByUserId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public bool TempDisplayUponLogin { get; set; }
        public int Id { get; set; }
    }

}
