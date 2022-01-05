using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reports.BLL.Models;
using Reports.BLL.Services;
using Reports.DAL.DTO.Body;
using Reports.DAL.Repository.Reports;

namespace Reports.BLL.Entities
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _repository;
        private readonly IMapper _mapper;
        public ReportsService(IReportsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReportModel> Create(AddReport addReport)
        {
            return _mapper.Map<ReportModel>(await _repository.Create(addReport));
        }

        public async Task<ReportModel> GetById(Guid id)
        {
            return _mapper.Map<ReportModel>(await _repository.GetById(id));
        }

        public async Task<List<ReportModel>> GetAll()
        {
            return _mapper.Map<List<ReportModel>>(await _repository.GetAll());
        }

        public async Task<List<ReportModel>> ApproveAll(Guid leadId)
        {
            return _mapper.Map<List<ReportModel>>(await _repository.ApproveAll(leadId));
        }

        public async Task<List<ReportModel>> GetSprintReports()
        {
            return _mapper.Map<List<ReportModel>>(await _repository.GetSprintReports());
        }

        public async Task<ReportModel> Delete(Guid id)
        {
            return _mapper.Map<ReportModel>(await _repository.Delete(id));
        }

        public async Task<ReportModel> Update(AddReport addReport, Guid id)
        {
            return _mapper.Map<ReportModel>(await _repository.Update(addReport, id));
        }
    }
}