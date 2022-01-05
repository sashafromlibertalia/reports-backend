using System;

namespace Reports.DAL.DTO.Query
{
    public class EditTask
    {
        public Guid NewEmployee { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}