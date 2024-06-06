using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IRequestCategoryService
    {
        public Task<ResponseApiDTO> CreateAsync(RequestCategoryDTO requestCategoryDTO);
        public Task<ResponseApiDTO<IEnumerable<RequestCategoryDTOWithId>>> GetAllAsync(string? search, string? sortBy, int page = 1);
        public Task<ResponseApiDTO<RequestCategoryDTOWithId>> GetByIdAsync(int id);
        public Task<ResponseApiDTO> UpdateAsync(RequestCategoryDTO requestCategoryDTO, int id);
        public Task<ResponseApiDTO> DeleteAsync(int id);
    }
}
