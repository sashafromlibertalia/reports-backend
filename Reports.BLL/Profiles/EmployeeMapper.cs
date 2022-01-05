using AutoMapper;
using Reports.BLL.Models;
using Reports.DAL.Entities;

namespace Reports.BLL.Profiles
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<EmployeeEntity, EmployeeModel>();
            CreateMap<EmployeeModel, EmployeeEntity>();
        }
    }
}