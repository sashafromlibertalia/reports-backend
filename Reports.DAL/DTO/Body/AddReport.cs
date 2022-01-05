using System;
using System.Collections.Generic;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO.Body
{
    public class AddReport
    {
        public Guid Author { get; set; }
        public Guid Sprint { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<TaskEntity> Tasks { get; set; }
        public List<ReportEntity> StaffReports { get; set; }
    }
}