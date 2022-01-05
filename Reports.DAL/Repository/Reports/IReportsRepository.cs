using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO.Body;
using Reports.DAL.Entities;

namespace Reports.DAL.Repository.Reports
{
    public interface IReportsRepository
    {
        Task<ReportEntity> Create(AddReport addReport);
        Task<ReportEntity> GetById(Guid reportId);
        Task<List<ReportEntity>> GetAll();
        Task<List<ReportEntity>> ApproveAll(Guid leadId);
        Task<List<ReportEntity>> GetSprintReports();
        Task<ReportEntity> Delete(Guid id);
        Task<ReportEntity> Update(AddReport addReport, Guid id);
    }
}