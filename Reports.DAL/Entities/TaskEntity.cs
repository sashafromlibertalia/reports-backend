using System;
using System.Collections.Generic;
using Reports.DAL.DTO.Body;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class TaskEntity
    {
        public TaskEntity()
        {
        }

        public TaskEntity(AddTask addTask)
        {
            if (string.IsNullOrWhiteSpace(addTask.Name))
                throw new ReportsException("Task name can't be empty");

            if (addTask.EmployeeId == Guid.Empty || addTask.Sprint == Guid.Empty)
                throw new ReportsException("Invalid sprint or assigner credential.");

            Name = addTask.Name;
            Description = addTask.Description;
            Id = Guid.NewGuid();
            EmployeeId = addTask.EmployeeId;
            Status = addTask.Status;
            Sprint = addTask.Sprint;
            Report = Guid.Empty;
            CreatedAt = DateTime.UtcNow.ToString("o");
            EditedAt = DateTime.UtcNow.ToString("o");
            Comments = new List<CommentEntity>();
        }
        public Guid Id { get; set; }
        public Guid Sprint { get; set; }
        public Guid Report { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string EditedAt { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}