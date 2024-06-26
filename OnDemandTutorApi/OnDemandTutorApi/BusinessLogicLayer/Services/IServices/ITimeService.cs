using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITimeService
    {
        public Task<ResponseApiDTO<TimeResponseDTO>> CreateAsync(TimeRequestDTO timeRequestDTO);
        public Task<ResponseApiDTO> CheckValidTime(TimeRequestDTO timeRequestDTO, int tutorId);
        public Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForStudentAsync(string userId, string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForTutorAsync(string userId, string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllAsync(string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int timeId, TimeRequestDTO timeRequest);
        public Task<ResponseApiDTO> DeleteAsync(int timeId);
    }
}
