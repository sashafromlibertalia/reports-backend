using AutoMapper;
using Reports.BLL.Models;
using Reports.DAL.Entities;

namespace Reports.BLL.Profiles
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<TaskEntity, TaskModel>();
            CreateMap<TaskModel, TaskEntity>();
        }
    }
}