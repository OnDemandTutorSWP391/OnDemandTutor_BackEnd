using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class SubjectLevelMapper : Profile
    {
        public SubjectLevelMapper()
        {
            CreateMap<SubjectLevel, SubjectLevelRequestDTO>().ReverseMap();
            CreateMap<SubjectLevel, SubjectLevelResponseDTO>().ReverseMap();
        }
    }
}
