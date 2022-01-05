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
    public class ReportsController : Controller
    {
        private readonly IReportsService _service;
        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ReportModel>> Create([FromBody] AddReport addReport)
        {
            try
            {
                return Ok(await _service.Create(addReport));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPatch("approve")]
        public async Task<ActionResult<List<ReportModel>>> ApproveAll([FromBody] Approve approve)
        {
            try
            {
                return Ok(await _service.ApproveAll(approve.LeadId));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReportModel>> GetReportById(Guid id)
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ReportModel>> GetAllReports()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("delete/{id:guid}")]
        public async Task<ActionResult<ReportModel>> DeleteReport(Guid id)
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

        [HttpPatch("update/{id:guid}")]
        public async Task<ActionResult<ReportModel>> UpdateReport([FromBody] AddReport addReport, Guid id)
        {
            try
            {
                return Ok(await _service.Update(addReport, id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}