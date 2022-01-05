using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.BLL.Models;
using Reports.DAL.DTO.Body;

namespace Reports.BLL.Services
{
    public interface IReportsService
    {
        Task<ReportModel> Create(AddReport addReport);
        Task<ReportModel> GetById(Guid id);
        Task<List<ReportModel>> GetAll();
        Task<List<ReportModel>> ApproveAll(Guid leadId);
        Task<List<ReportModel>> GetSprintReports();
        Task<ReportModel> Delete(Guid id);
        Task<ReportModel> Update(AddReport addReport, Guid id);
    }
}