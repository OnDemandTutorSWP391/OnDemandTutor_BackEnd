using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class ResponseMapper : Profile
    {
        public ResponseMapper()
        {
            CreateMap<Response, ResponseDTO>().ReverseMap();
        }
    }
}
