using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Reports.DAL.Context;
using Reports.DAL.DTO.Body;
using Reports.DAL.Entities;
using Reports.DAL.Tools;

namespace Reports.DAL.Repository.Sprints
{
    public class SprintsRepository : ISprintsRepository
    {
        private readonly ReportsContext _context;
        public SprintsRepository(ReportsContext context) {
            _context = context;
        }

        public async Task<List<SprintEntity>> GetAll()
        {
            return await _context.Sprints.Include(item => item.ApprovedReports).ToListAsync();
        }

        public async Task<SprintEntity> GetById(Guid id)
        {
            SprintEntity sprint = await _context.Sprints
                .Include(item => item.ApprovedReports)
                .SingleOrDefaultAsync(item => item.Id == id);

            if (sprint == null)
                throw new ArgumentNullException(string.Empty, "Sprint is null.");

            return sprint;
        }

        public async Task<SprintEntity> GetCurrent()
        {
            SprintEntity currentSprint = await _context.Sprints
                .Include(item => item.ApprovedReports)
                .SingleOrDefaultAsync(item => item.IsActive);

            if (currentSprint == null)
                throw new ArgumentNullException(string.Empty, "Current sprint is null.");

            return currentSprint;
        }

        public async Task<SprintEntity> Create(AddSprint addSprint)
        {
            SprintEntity activeSprint = await _context.Sprints.SingleOrDefaultAsync(item => item.IsActive);
            if (activeSprint != null)
                throw new ReportsException("Can't create second active sprint");

            var sprint = new SprintEntity(addSprint.StartDate, addSprint.EndDate);
            foreach (EmployeeEntity employee in await _context.Employees.ToListAsync())
            {
                employee.HasReport = false;
            }

            await _context.Sprints.AddAsync(sprint);
            await _context.SaveChangesAsync();

            return sprint;
        }
    }
}