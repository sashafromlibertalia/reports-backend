using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.BLL.Models;
using Reports.DAL.DTO.Body;

namespace Reports.BLL.Services
{
    public interface ISprintsService
    {
        Task<List<SprintModel>> GetAll();
        Task<SprintModel> GetById(Guid id);
        Task<SprintModel> GetCurrent();
        Task<SprintModel> Create(AddSprint addSprint);
    }
}