using System;
using System.Collections.Generic;

namespace Reports.BLL.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public Guid Sprint { get; set; }
        public Guid Report { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string EditedAt { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}