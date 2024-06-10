using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class TimeMapper : Profile
    {
        public TimeMapper()
        {
            CreateMap<Time, TimeRequestDTO>().ReverseMap();
            CreateMap<Time, TimeResponseDTO>().ReverseMap();
        }
    }
}
