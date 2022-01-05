using System;

namespace Reports.DAL.DTO.Body
{
    public class AddEmployee
    {
        public Guid Boss { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }
}