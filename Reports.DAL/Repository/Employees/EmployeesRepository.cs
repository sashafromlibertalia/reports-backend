using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Context;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.DAL.Types;

namespace Reports.DAL.Repository.Employees
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ReportsContext _context;
        public EmployeesRepository(ReportsContext context) {
            _context = context;
        }

        public async Task<EmployeeEntity> Create(AddEmployee addEmployee)
        {
            var employee = new EmployeeEntity(addEmployee.Name, addEmployee.Age, addEmployee.Role);

            switch (addEmployee.Role)
            {
                case Roles.Manager:
                    EmployeeEntity lead = await _context.Employees.SingleOrDefaultAsync(item => item.Role == Roles.Lead);
                    if (lead == null)
                        throw new ReportsException("Create lead first.");

                    lead.Staff.Add(employee);
                    employee.Boss = lead.Id;
                    break;
                case Roles.Worker:
                    EmployeeEntity manager = await _context.Employees.SingleOrDefaultAsync(item => item.Id == addEmployee.Boss);
                    if (manager == null)
                        throw new ReportsException("Invalid manager credentials.");

                    manager.Staff.Add(employee);
                    employee.Boss = addEmployee.Boss;
                    break;
                case Roles.Lead:
                    EmployeeEntity teamLead = await _context.Employees.SingleOrDefaultAsync(item => item.Role == Roles.Lead);
                    if (teamLead != null)
                        throw new ReportsException("Can't create two leads.");
                    break;
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeEntity> GetById(Guid id)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .FirstOrDefaultAsync(item => item.Id == id);
            if (employee == null)
                throw new ReportsException("Employee is null.");

            return employee;
        }

        public async Task<List<EmployeeEntity>> GetAll()
        {
            return await _context.Employees.Include(item => item.Tasks).ToListAsync();
        }

        public async Task<List<EmployeeEntity>> GetStaffWithReports(Guid bossId)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments).
                SingleOrDefaultAsync(item => item.Id == bossId);

            return employee.Staff.Where(item => item.HasReport)
                .ToList();
        }

        public async Task<List<EmployeeEntity>> GetStaffWithoutReports(Guid bossId)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .SingleOrDefaultAsync(item => item.Id == bossId);

            return employee.Staff.Where(item => item.HasReport == false)
                .ToList();
        }

        public async Task<List<EmployeeEntity>> GetStaff(Guid id)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .SingleOrDefaultAsync(item => item.Id == id);

            return employee.Staff;
        }

        public async Task<EmployeeEntity> Delete(Guid id)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .FirstOrDefaultAsync(item => item.Id == id);

            EmployeeEntity lead = await _context.Employees.SingleOrDefaultAsync(item => item.Role == Roles.Lead);
            if (lead == null)
                throw new ReportsException("Create lead first.");

            if (employee == null)
                throw new ReportsException("Employee is null.");

            lead.Staff.AddRange(employee.Staff);
            foreach (EmployeeEntity user in employee.Staff)
            {
                user.Boss = lead.Id;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeEntity> Update(EditEmployee editEmployee, Guid id)
        {
            EmployeeEntity employee = await _context.Employees
                .Include(item => item.Staff)
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (employee == null)
                throw new ReportsException("Task not found.");

            if (!string.IsNullOrWhiteSpace(editEmployee.Name))
                employee.Name = editEmployee.Name;

            if (editEmployee.Age > 0)
                employee.Age = editEmployee.Age;

            await _context.SaveChangesAsync();
            return employee;
        }
    }
}