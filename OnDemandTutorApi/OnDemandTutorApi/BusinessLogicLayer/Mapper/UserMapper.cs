using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserRequestDTO>().ReverseMap();
            CreateMap<User, UserGetProfileDTO>().ReverseMap();
            CreateMap<UserProfileUpdateDTO, User>().ReverseMap();
        }
    }
}
