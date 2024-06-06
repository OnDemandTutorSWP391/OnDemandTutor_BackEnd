using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IRequestService
    {
        public Task<ResponseApiDTO> CreateAsync(string userId, string categoryName, RequestDTO requestDTO);
        public Task<ResponseApiDTO<IEnumerable<RequestDTOWithUserData>>> UserGetAllAsync(string userId, string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<RequestDTOWithData>>> GetAllAsync(string? search, string? userId, DateTime? from, DateTime? to, string? sortBy, int page = 1);
        public Task<ResponseApiDTO> UpdateStatusRequestAsync(int id, RequestUpdateStatusDTO requestUpdate);
    }
}
