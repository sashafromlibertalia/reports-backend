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

namespace Reports.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksService _service;
        public TasksController(ITasksService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<TaskModel>>> GetAllTasks()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskModel>> GetSingleTask(Guid id)
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

        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddTask([FromBody] AddTask newTaskData)
        {
            try
            {
                return Ok(await _service.Create(newTaskData));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("remove/{id:guid}")]
        public async Task<ActionResult<TaskModel>> RemoveTask(Guid id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpPatch("edit/{id:guid}")]
        public async Task<ActionResult<TaskModel>> EditTask([FromQuery] EditTask editedTask, Guid id)
        {
            try
            {
                return Ok(await _service.Update(editedTask, id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("by/{id:guid}")]
        public async Task<ActionResult<List<TaskModel>>> GetTasksByEmployee(Guid id)
        {
            try
            {
                return Ok(await _service.GetAllByEmployeeId(id));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}