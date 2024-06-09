using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ILevelService
    {
        public Task<ResponseApiDTO> CreateAsync(LevelDTO levelDTO);
        public Task<ResponseApiDTO<IEnumerable<LevelDTOWithId>>> GetAllAsync(string? search, string? levelId, string? sortBy, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int id, LevelDTO levelDTO);
    }
}
