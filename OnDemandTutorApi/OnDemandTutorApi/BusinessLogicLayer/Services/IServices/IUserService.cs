using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IUserService
    {
        public Task<ResponseApiDTO> SignUpAsync(UserRequestDTO userRequestDTO);
        public Task<ResponseApiDTO<TokenDTO>> SignInAsync(UserAuthenDTO userAuthen);
        public Task<ResponseApiDTO<TokenDTO>> RenewTokenAsync(TokenDTO tokenDTO);
        public Task<ResponseApiDTO> ResetPassAsync(UserResetPassDTO userReset);
        public Task<ResponseApiDTO<UserGetProfileDTO>> GetUserProfileAysnc(string id);
        public Task<ResponseApiDTO<UserGetProfileDTO>> UpdatUserProfileAsync(string id, UserProfileUpdateDTO userUpdate);
        public Task<ResponseApiDTO> UpdateUserStatusAsync(string userId, bool status);
    }
}
