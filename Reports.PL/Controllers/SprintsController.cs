using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;

namespace Reports.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SprintsController : Controller
    {
        private readonly ISprintsService _service;
        public SprintsController(ISprintsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<SprintModel>>> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpGet("current")]
        public async Task<ActionResult<SprintModel>> GetCurrent()
        {
            try
            {
                return Ok(await _service.GetCurrent());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SprintModel>> Get(Guid id)
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
        public async Task<ActionResult<SprintModel>> Create([FromBody] AddSprint addSprint)
        {
            try
            {
                return Ok(await _service.Create(addSprint));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}