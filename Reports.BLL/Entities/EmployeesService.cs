using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;
using Reports.DAL.Repository.Employees;

namespace Reports.BLL.Entities
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;
        private readonly IMapper _mapper;
        public EmployeesService(IEmployeesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Create(AddEmployee addEmployee)
        {
            EmployeeEntity employeeEntity = await _repository.Create(addEmployee);
            return _mapper.Map<EmployeeModel>(employeeEntity);
        }

        public async Task<EmployeeModel> GetById(Guid employeeId)
        {
            EmployeeEntity employeeEntity = await _repository.GetById(employeeId);
            return _mapper.Map<EmployeeModel>(employeeEntity);
        }

        public async Task<List<EmployeeModel>> GetStaff(Guid bossId)
        {
            return _mapper.Map<List<EmployeeModel>>(await _repository.GetStaff(bossId));
        }

        public async Task<List<EmployeeModel>> GetStaffWithReports(Guid bossId)
        {
            return _mapper.Map<List<EmployeeModel>>(await _repository.GetStaffWithReports(bossId));
        }

        public async Task<List<EmployeeModel>> GetStaffWithoutReports(Guid bossId)
        {
            return _mapper.Map<List<EmployeeModel>>(await _repository.GetStaffWithoutReports(bossId));
        }

        public async Task<List<EmployeeModel>> GetAll()
        {
            return _mapper.Map<List<EmployeeModel>>(await _repository.GetAll());
        }

        public async Task<EmployeeModel> Delete(Guid employeeId)
        {
            return _mapper.Map<EmployeeModel>(await _repository.Delete(employeeId));
        }

        public async Task<EmployeeModel> Update(EditEmployee editEmployee, Guid id)
        {
            return _mapper.Map<EmployeeModel>(await _repository.Update(editEmployee, id));
        }
    }
}