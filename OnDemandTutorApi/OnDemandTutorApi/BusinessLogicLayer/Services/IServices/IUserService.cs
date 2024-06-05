using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IUserService
    {
        public Task<ResponseDTO> SignUpAsync(UserRequestDTO userRequestDTO);
        public Task<ResponseDTO<TokenDTO>> SignInAsync(UserAuthenDTO userAuthen);
        public Task<ResponseDTO<TokenDTO>> RenewTokenAsync(TokenDTO tokenDTO);
        public Task<ResponseDTO> ResetPassAsync(UserResetPassDTO userReset);
        public Task<ResponseDTO<UserGetProfileDTO>> GetUserProfileAysnc(string id);
        public Task<ResponseDTO<UserGetProfileDTO>> UpdatUserProfileAsync(string id, UserProfileUpdateDTO userUpdate);
    }
}
