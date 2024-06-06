using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IResponseService
    {
        public Task<ResponseApiDTO> SendResponseAsync(int requestId, ResponseContentDTO responseContent);
        public Task<ResponseApiDTO<IEnumerable<ResponseDTOWithData>>> GetAllAsync(string? search, string? requestId, DateTime? from, DateTime? to, string? sortBy, int page = 1);
    }
}
