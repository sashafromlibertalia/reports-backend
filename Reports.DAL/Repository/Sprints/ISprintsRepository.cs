using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO.Body;
using Reports.DAL.Entities;

namespace Reports.DAL.Repository.Sprints
{
    public interface ISprintsRepository
    {
        Task<List<SprintEntity>> GetAll();
        Task<SprintEntity> GetById(Guid id);
        Task<SprintEntity> GetCurrent();
        Task<SprintEntity> Create(AddSprint addSprint);
    }
}