using System;
using System.Collections.Generic;
using Reports.DAL.Tools;
using Reports.DAL.Types;

namespace Reports.DAL.Entities
{
    public class EmployeeEntity
    {
        private const int MinimalEmployeeAge = 14;
        private const int MaximumEmployeeAge = 80;

        public EmployeeEntity(string name, int age, string role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(string.Empty, "Invalid name.");

            if (age is < MinimalEmployeeAge or > MaximumEmployeeAge)
                throw new ReportsException("Invalid employee's age.");

            if (role is not (Roles.Manager or Roles.Worker or Roles.Lead))
                throw new ReportsException("Invalid employee's role.");

            Name = name;
            Age = age;
            Id = Guid.NewGuid();
            Boss = Guid.Empty;
            Role = role;
            CreatedAt = DateTime.UtcNow.ToString("o");
            Tasks = new List<TaskEntity>();
            Staff = new List<EmployeeEntity>();
            HasReport = false;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasReport { get; set; }
        public Guid Boss { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string CreatedAt { get; }
        public List<TaskEntity> Tasks { get; set; }
        public List<EmployeeEntity> Staff { get; set; }
    }
}