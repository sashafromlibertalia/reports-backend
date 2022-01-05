using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class SprintEntity
    {
        public SprintEntity(string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                throw new ArgumentNullException(string.Empty, "Sprint dates are null.");

            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            IsActive = true;
            ApprovedReports = new List<ReportEntity>();
        }

        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<ReportEntity> ApprovedReports { get; set; }
    }
}