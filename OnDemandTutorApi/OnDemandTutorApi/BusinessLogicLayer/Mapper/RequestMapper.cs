using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<Request, RequestDTOWithData>().ReverseMap();
            CreateMap<Request, RequestDTOWithUserData>().ReverseMap();
            CreateMap<Request, RequestUpdateStatusDTO>().ReverseMap();
        }
    }
}
