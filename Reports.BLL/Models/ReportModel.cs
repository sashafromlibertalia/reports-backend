using System;
using System.Collections.Generic;

namespace Reports.BLL.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public Guid Sprint { get; set; }
        public Guid Author { get; set; }
        public Guid RefersToAuthor { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<ReportModel> StaffReports { get; set; }
        public string CreatedAt { get; set; }
        public string EditedAt { get; set; }
    }
}