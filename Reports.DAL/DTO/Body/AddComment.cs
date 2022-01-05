using System;

namespace Reports.DAL.DTO.Body
{
    public class AddComment
    {
        public string Message { get; set; }
        public Guid Author { get; set; }
    }
}