using AutoMapper;
using Reports.BLL.Models;
using Reports.DAL.Entities;

namespace Reports.BLL.Profiles
{
    public class SprintsMapper : Profile
    {
        public SprintsMapper()
        {
            CreateMap<SprintEntity, SprintModel>();
            CreateMap<SprintModel, SprintEntity>();
        }
    }
}