using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IAdminService
    {
        public Task<ResponseDTO> CreateUserAsync(UserRequestDTO userRequest);
        public Task<ResponseDTO<IEnumerable<UserResponseDTO>>> GetUsersAsync(string? search, string? sortBy, int pageIndex = 1);
        public Task<ResponseDTO> UpdateUserAsync(string id, UserUpdateDTO userUpdateDTO);
        public Task<ResponseDTO> UpdateUserRoleAsync(string id, string oldRole, string newRole, string choice);
        public Task<ResponseDTO> DeleteUserAsync(string id);
        public Task<ResponseDTO<IEnumerable<CoinDTOWithId>>> GetTransactionsAsync(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1);
    }
}
