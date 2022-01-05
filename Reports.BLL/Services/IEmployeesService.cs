using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.BLL.Models;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;

namespace Reports.BLL.Services
{
    public interface IEmployeesService
    {
        Task<EmployeeModel> Create(AddEmployee addEmployee);
        Task<EmployeeModel> GetById(Guid employeeId);
        Task<List<EmployeeModel>> GetStaff(Guid bossId);
        Task<List<EmployeeModel>> GetStaffWithReports(Guid bossId);
        Task<List<EmployeeModel>> GetStaffWithoutReports(Guid bossId);
        Task<List<EmployeeModel>> GetAll();
        Task<EmployeeModel> Delete(Guid employeeId);
        Task<EmployeeModel> Update(EditEmployee editEmployee, Guid id);
    }
}