using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class LevelMapper : Profile
    {
        public LevelMapper()
        {
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<Level, LevelDTOWithId>().ReverseMap();
        }
    }
}
