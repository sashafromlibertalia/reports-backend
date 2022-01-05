using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;
using Reports.DAL.Repository.Tasks;

namespace Reports.BLL.Entities
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repository;
        private readonly IMapper _mapper;
        public TasksService(ITasksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TaskModel> Create(AddTask addTask)
        {
            TaskEntity taskFromDb = await _repository.Create(addTask);
            TaskModel task = _mapper.Map<TaskModel>(taskFromDb);
            return task;
        }

        public async Task<TaskModel> GetById(Guid id)
        {
            return _mapper.Map<TaskModel>(await _repository.GetById(id));
        }

        public async Task<TaskModel> GetByCreationDate(string creationDate)
        {
            return _mapper.Map<TaskModel>(await _repository.GetByCreationDate(creationDate));
        }

        public async Task<TaskModel> GetByEditingDate(string editedDate)
        {
            return _mapper.Map<TaskModel>(await _repository.GetByEditingDate(editedDate));
        }

        public async Task<List<TaskModel>> GetEditedBy(Guid employee)
        {
            return _mapper.Map<List<TaskModel>>(await _repository.GetEditedBy(employee));
        }

        public async Task<List<TaskModel>> GetAssignedBy(Guid employee)
        {
            return _mapper.Map<List<TaskModel>>(await _repository.GetAssignedBy(employee));
        }

        public async Task<List<TaskModel>> GetAll()
        {
            List<TaskEntity> dbListTasks = await _repository.GetAll();
            return _mapper.Map<List<TaskModel>>(dbListTasks);
        }

        public async Task<List<TaskModel>> GetAllForSprint(Guid id)
        {
            return _mapper.Map<List<TaskModel>>(await _repository.GetAllForSprint(id));
        }

        public async Task<TaskModel> Delete(Guid id)
        {
            return _mapper.Map<TaskModel>(await _repository.Delete(id));
        }

        public async Task<TaskModel> Update(EditTask editTask, Guid id)
        {
            return _mapper.Map<TaskModel>(await _repository.Update(editTask, id));
        }

        public async Task<List<TaskModel>> GetAllByEmployeeId(Guid id)
        {
            return _mapper.Map<List<TaskModel>>(await _repository.GetAllByEmployeeId(id));
        }
    }
}