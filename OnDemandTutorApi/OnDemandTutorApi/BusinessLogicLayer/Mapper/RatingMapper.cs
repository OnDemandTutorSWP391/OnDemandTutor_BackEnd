using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class RatingMapper : Profile
    {
        public RatingMapper()
        {
            CreateMap<Rating, RatingRequestDTO>().ReverseMap();
            CreateMap<Rating, RatingResponseDTO>().ReverseMap();
            CreateMap<Rating, RatingUpdateDTO>().ReverseMap();
        }
    }
}
