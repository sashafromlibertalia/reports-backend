using System;

namespace Reports.DAL.DTO.Body
{
    public class AddTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid Sprint { get; set; }
        public string Status { get; set; }
    }
}