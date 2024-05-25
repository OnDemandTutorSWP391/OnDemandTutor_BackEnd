using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class TutorMapper : Profile
    {
        public TutorMapper()
        {
            CreateMap<Tutor, TutorDTO>().ReverseMap();
        }
    }
}
