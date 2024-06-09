using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class SubjectMapper : Profile
    {
        public SubjectMapper()
        {
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTOWithId>().ReverseMap();
        }
    }
}
