using AutoMapper;
using Reports.BLL.Models;
using Reports.DAL.Entities;

namespace Reports.BLL.Profiles
{
    public class ReportsMapper : Profile
    {
        public ReportsMapper()
        {
            CreateMap<ReportEntity, ReportModel>();
            CreateMap<ReportModel, ReportEntity>();
        }
    }
}