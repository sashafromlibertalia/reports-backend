using System;
using System.Collections.Generic;
using Reports.DAL.DTO.Body;
using Reports.DAL.Tools;
using Reports.DAL.Types;

namespace Reports.DAL.Entities
{
    public class ReportEntity
    {
        public ReportEntity()
        {
        }

        public ReportEntity(AddReport addReport)
        {
            if (addReport.Author == Guid.Empty)
                throw new ReportsException("Invalid report's author credentials.");

            if (addReport.Status != ReportStatus.Approved && addReport.Status != ReportStatus.Draft &&
                addReport.Status != ReportStatus.Submitted)
                throw new ReportsException("Invalid report status.");

            Id = Guid.NewGuid();
            Description = addReport.Description;
            Sprint = addReport.Sprint;
            Status = addReport.Status;
            Author = addReport.Author;
            Tasks = addReport.Tasks;
            RefersToAuthor = Guid.Empty;
            StaffReports = new List<ReportEntity>();
            CreatedAt = DateTime.UtcNow.ToString("o");
            EditedAt = DateTime.UtcNow.ToString("o");
        }

        public Guid Id { get; set; }
        public Guid Sprint { get; set; }
        public Guid Author { get; set; }
        public Guid RefersToAuthor { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<TaskEntity> Tasks { get; set; }
        public List<ReportEntity> StaffReports { get; set; }
        public string CreatedAt { get; set; }
        public string EditedAt { get; set; }
    }
}