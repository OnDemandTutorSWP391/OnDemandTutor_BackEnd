using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class CoinManagementMapper : Profile
    {
        public CoinManagementMapper()
        {
            CreateMap<CoinManagement, CoinDTO>().ReverseMap();
            CreateMap<CoinManagement, CoinDTOWithId>().ReverseMap();
        }
    }
}
