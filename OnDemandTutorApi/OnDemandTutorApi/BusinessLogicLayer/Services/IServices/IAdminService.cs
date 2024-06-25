using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IAdminService
    {
        public Task<ResponseApiDTO> CreateUserAsync(UserRequestDTO userRequest);
        public Task<ResponseApiDTO<IEnumerable<UserResponseDTO>>> GetUsersAsync(string? search, string? sortBy, int pageIndex = 1);
        public Task<ResponseApiDTO> UpdateUserAsync(string id, UserUpdateDTO userUpdateDTO);
        public Task<ResponseApiDTO> UpdateUserRoleAsync(string id, string oldRole, string newRole, string choice);
        public Task<ResponseApiDTO> DeleteUserAsync(string id);
        public Task<ResponseApiDTO> LockUserAsync(string id);
        public Task<ResponseApiDTO<IEnumerable<CoinDTOWithId>>> GetTransactionsAsync(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1);
    }
}
