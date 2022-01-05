using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.PresentationLayer.Types;

namespace Reports.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _service;
        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<EmployeeModel>>> GetAllEmployees()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<EmployeeModel>> GetSingleEmployee(Guid id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpGet("staff/{id:guid}")]
        public async Task<ActionResult<List<EmployeeModel>>> GetStaff([FromQuery] string type, Guid id)
        {
            try
            {
                switch (type)
                {
                    case StaffTypes.All:
                        return Ok(await _service.GetStaff(id));
                    case StaffTypes.WithReports:
                        return Ok(await _service.GetStaffWithReports(id));
                    case StaffTypes.WithoutReports:
                        return Ok(await _service.GetStaffWithoutReports(id));
                    default:
                        return Ok(await _service.GetStaff(id));
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<EmployeeModel>> AddEmployee([FromBody] AddEmployee newEmployee)
        {
            try
            {
                return Ok(await _service.Create(newEmployee));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("remove/{id:guid}")]
        public async Task<ActionResult<EmployeeModel>> RemoveEmployee(Guid id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<EmployeeModel>> EditEmployee([FromQuery] EditEmployee editEmployee, Guid id)
        {
            try
            {
                return Ok(await _service.Update(editEmployee, id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}