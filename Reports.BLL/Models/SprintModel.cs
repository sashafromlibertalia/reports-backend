using System;
using System.Collections.Generic;

namespace Reports.BLL.Models
{
    public class SprintModel
    {
        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<ReportModel> ApprovedReports { get; set; }
    }
}