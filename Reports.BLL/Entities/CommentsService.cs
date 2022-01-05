using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Repository.Comments;

namespace Reports.BLL.Entities
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _repository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CommentModel>> GetAll()
        {
            return _mapper.Map<List<CommentModel>>(await _repository.GetAll());
        }

        public async Task<List<CommentModel>> GetForTask(Guid id)
        {
            return _mapper.Map<List<CommentModel>>(await _repository.GetForTask(id));
        }

        public async Task<CommentModel> Create(TaskForComment taskForComment, AddComment addComment)
        {
            return _mapper.Map<CommentModel>(await _repository.Create(taskForComment, addComment));
        }
    }
}