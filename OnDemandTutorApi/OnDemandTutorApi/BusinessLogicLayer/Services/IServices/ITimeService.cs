using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITimeService
    {
        public Task<ResponseApiDTO<TimeResponseDTO>> CreateAsync(TimeRequestDTO timeRequestDTO);
        public Task<ResponseApiDTO> CheckValidTime(TimeRequestDTO timeRequestDTO, int tutorId);
        public Task<ResponseApiDTO<IEnumerable<TimeResponseDTO>>> GetAllForStudentAsync(string userId, string? timeId, string? sortBy, DateTime? from, DateTime? to, int page = 1);
    }
}
