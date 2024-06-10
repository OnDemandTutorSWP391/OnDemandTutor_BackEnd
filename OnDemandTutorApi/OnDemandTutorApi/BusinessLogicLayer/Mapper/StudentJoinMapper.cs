using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class StudentJoinMapper : Profile
    {
        public StudentJoinMapper()
        {
            CreateMap<StudentJoin, StudentJoinDTO>().ReverseMap();
        }
    }
}
