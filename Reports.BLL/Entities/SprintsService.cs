using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.Repository.Sprints;

namespace Reports.BLL.Entities
{
    public class SprintsService : ISprintsService
    {
        private readonly ISprintsRepository _repository;
        private readonly IMapper _mapper;
        public SprintsService(ISprintsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SprintModel>> GetAll()
        {
            return _mapper.Map<List<SprintModel>>(await _repository.GetAll());
        }

        public async Task<SprintModel> GetById(Guid id)
        {
            return _mapper.Map<SprintModel>(await _repository.GetById(id));
        }

        public async Task<SprintModel> GetCurrent()
        {
            return _mapper.Map<SprintModel>(await _repository.GetCurrent());
        }

        public async Task<SprintModel> Create(AddSprint addSprint)
        {
            return _mapper.Map<SprintModel>(await _repository.Create(addSprint));
        }
    }
}