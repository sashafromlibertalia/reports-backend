using AutoMapper;
using Reports.BLL.Models;
using Reports.DAL.Entities;

namespace Reports.BLL.Profiles
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<CommentEntity, CommentModel>();
            CreateMap<CommentModel, CommentEntity>();
        }
    }
}