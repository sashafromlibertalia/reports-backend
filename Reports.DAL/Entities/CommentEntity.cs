using System;

namespace Reports.DAL.Entities
{
    public class CommentEntity
    {
        public CommentEntity()
        {}
        public CommentEntity(Guid task, Guid employeeId, string message)
        {
            if (task == Guid.Empty)
                throw new ArgumentNullException(string.Empty, "Can't create comment without task ID");

            Id = Guid.NewGuid();
            Task = task;
            Author = employeeId;
            Message = message;
            CreatedAt = DateTime.UtcNow.ToString("o");
        }

        public Guid Id { get; set; }
        public Guid Task { get; set; }
        public Guid Author { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
    }
}