using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IUserService
    {
        public Task<ResponseDTO> SignUpAsync(UserRequestDTO userRequestDTO);
        public Task<ResponseDTO<TokenDTO>> SignInAsync(UserAuthenDTO userAuthen);
        public Task<ResponseDTO<TokenDTO>> RenewTokenAsync(TokenDTO tokenDTO);
        public Task<ResponseDTO> ResetPassAsync(UserResetPassDTO userReset);
    }
}
