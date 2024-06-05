using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IRequestService
    {
        public Task<ResponseDTO> CreateRequestAsync(RequestDTO userRequest);
        public Task<ResponseDTO<IEnumerable<RequestDTO>>> GetAllRequestAsync(string? search, string? sortBy, int pageIndex = 1);
        public Task<ResponseDTO<IEnumerable<RequestDTO>>> GetRequestByUserIDAsync(string UserID, string? search, string? sortBy, int pageIndex = 1);
        public Task<ResponseDTO<IEnumerable<RequestDTO>>> GetRequestByRequestIDAsync(int id,string? search, string? sortBy, int pageIndex = 1);
        public Task<ResponseDTO> UpdateRequestAsync(int id,RequestDTO userRequest);
        public Task<ResponseDTO> DeleteRequestAsync(int id);
    }
}