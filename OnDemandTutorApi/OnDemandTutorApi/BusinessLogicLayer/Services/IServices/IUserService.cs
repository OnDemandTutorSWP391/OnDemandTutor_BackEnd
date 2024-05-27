using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IUserService
    {
        public Task<ResponseDTO<IdentityResult>> SignUpAsync(UserRequestDTO userRequestDTO);
        public Task<ResponseDTO<TokenDTO>> SignInAsync(UserAuthenDTO userAuthen);

        public Task<ResponseDTO<TokenDTO>> RenewTokenAsync(TokenDTO tokenDTO);
    }
}
