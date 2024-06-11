using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ISubjectLevelService
    {
        public Task<ResponseApiDTO<SubjectLevelResponseDTO>> CreateAsync(string userId, SubjectLevelRequestDTO subjectLevelDTO);
        public Task<ResponseApiDTO<IEnumerable<SubjectLevelResponseDTO>>> GetAllAsync(string? level, string? subject, string? tutor, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int id, string userId, SubjectLevelRequestDTO subjectLevelDTO);
    }
}
