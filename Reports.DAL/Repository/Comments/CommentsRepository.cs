using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Reports.DAL.Context;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;
using Reports.DAL.Tools;

namespace Reports.DAL.Repository.Comments
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ReportsContext _context;
        public CommentsRepository(ReportsContext context) {
            _context = context;
        }

        public async Task<List<CommentEntity>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<CommentEntity>> GetForTask(Guid id)
        {
            return await _context.Comments.Where(item => item.Task == id).ToListAsync();
        }

        public async Task<CommentEntity> Create(TaskForComment taskForComment, AddComment addComment)
        {
            if (taskForComment.Task == Guid.Empty || addComment.Author == Guid.Empty)
                throw new ReportsException("Invalid comment credentials.");

            var comment = new CommentEntity(taskForComment.Task, addComment.Author, addComment.Message);
            TaskEntity task = _context.Tasks.Include(item => item.Comments)
                .SingleOrDefault(item => item.Id == taskForComment.Task);
            if (task == null)
                throw new ReportsException("Task not found.");

            task.Comments.Add(comment);
            _context.Entry(task).State = EntityState.Modified;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}