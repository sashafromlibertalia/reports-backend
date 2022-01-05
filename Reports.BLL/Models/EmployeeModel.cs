using System;
using System.Collections.Generic;

namespace Reports.BLL.Models
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public Guid Boss { get; set; }
        public bool HasReport { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string CreatedAt { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<EmployeeModel> Staff { get; set; }
    }
}