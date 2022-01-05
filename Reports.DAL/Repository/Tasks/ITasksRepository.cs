using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;

namespace Reports.DAL.Repository.Tasks
{
    public interface ITasksRepository
    {
        Task<TaskEntity> Create(AddTask addTask);
        Task<TaskEntity> GetById(Guid id);
        Task<List<TaskEntity>> GetAll();
        Task<TaskEntity> Delete(Guid id);
        Task<TaskEntity> GetByCreationDate(string creationDate);
        Task<TaskEntity> GetByEditingDate(string editedDate);
        Task<List<TaskEntity>> GetEditedBy(Guid employee);
        Task<List<TaskEntity>> GetAssignedBy(Guid employee);
        Task<TaskEntity> Update(EditTask editTask, Guid id);
        Task<List<TaskEntity>> GetAllByEmployeeId(Guid employeeId);
        Task<List<TaskEntity>> GetAllForSprint(Guid sprintId);
    }
}