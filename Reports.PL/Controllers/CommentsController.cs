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
    public class CommentsController : Controller
    {
        private readonly ICommentsService _service;
        public CommentsController(ICommentsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentModel>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<CommentModel>>> GetForTask(Guid id)
        {
            return Ok(await _service.GetForTask(id));
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> AddComment([FromQuery] TaskForComment taskForComment, [FromBody] AddComment addComment)
        {
            try
            {
                return Ok(await _service.Create(taskForComment, addComment));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}