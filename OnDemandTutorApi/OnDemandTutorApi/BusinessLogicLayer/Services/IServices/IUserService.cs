using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IUserService
    {
        public Task<IdentityResult> SignUpAsync(UserRequestDTO userRequestDTO);
        public Task<string> SignInAsync(UserAuthenDTO userAuthen);
        public Task<IdentityResult> UpdateProfileUserAsync(UserProfileUpdateDTO userDTO, string userId);
        public Task<UserProfileUpdateDTO> GetUserProfileAsync(string userId);  
    }
}
