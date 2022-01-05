using System;

namespace Reports.BLL.Models
{
    public class CommentModel

    {
        public Guid Id { get; set; }
        public Guid Task { get; set; }
        public Guid Author { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
    }
}