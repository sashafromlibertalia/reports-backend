using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.BLL.Models;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;

namespace Reports.BLL.Services
{
    public interface ITasksService
    {
        Task<TaskModel> Create(AddTask addTask);
        Task<TaskModel> GetById(Guid id);
        Task<List<TaskModel>> GetAll();
        Task<List<TaskModel>> GetAllForSprint(Guid id);
        Task<TaskModel> GetByCreationDate(string creationDate);
        Task<TaskModel> GetByEditingDate(string editedDate);
        Task<List<TaskModel>> GetEditedBy(Guid employee);
        Task<List<TaskModel>> GetAssignedBy(Guid employee);
        Task<TaskModel> Delete(Guid id);
        Task<TaskModel> Update(EditTask editTask, Guid id);
        Task<List<TaskModel>> GetAllByEmployeeId(Guid id);
    }
}