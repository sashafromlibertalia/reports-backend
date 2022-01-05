using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;

namespace Reports.DAL.Repository.Employees
{
    public interface IEmployeesRepository
    {
        Task<EmployeeEntity> Create(AddEmployee addEmployee);
        Task<EmployeeEntity> GetById(Guid id);
        Task<List<EmployeeEntity>> GetStaff(Guid id);
        Task<List<EmployeeEntity>> GetAll();
        Task<List<EmployeeEntity>> GetStaffWithReports(Guid bossId);
        Task<List<EmployeeEntity>> GetStaffWithoutReports(Guid bossId);
        Task<EmployeeEntity> Delete(Guid id);
        Task<EmployeeEntity> Update(EditEmployee editEmployee, Guid id);
    }
}