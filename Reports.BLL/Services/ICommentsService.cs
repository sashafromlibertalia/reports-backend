using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.BLL.Models;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;

namespace Reports.BLL.Services
{
    public interface ICommentsService
    {
        Task<List<CommentModel>> GetAll();
        Task<List<CommentModel>> GetForTask(Guid id);
        Task<CommentModel> Create(TaskForComment taskForComment, AddComment addComment);
    }
}