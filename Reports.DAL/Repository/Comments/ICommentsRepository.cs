using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;

namespace Reports.DAL.Repository.Comments
{
    public interface ICommentsRepository
    {
        Task<List<CommentEntity>> GetAll();
        Task<List<CommentEntity>> GetForTask(Guid id);
        Task<CommentEntity> Create(TaskForComment taskForComment, AddComment addComment);
    }
}