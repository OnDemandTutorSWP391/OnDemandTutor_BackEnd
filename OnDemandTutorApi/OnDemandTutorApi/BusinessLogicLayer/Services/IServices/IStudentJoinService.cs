using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IStudentJoinService
    {
        public Task<ResponseApiDTO<StudentJoinResponseDTO>> CreateAsync(StudentJoinRequestDTO studentJoinDTO);
        public Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetBySubjectLevelIdAsync(string subjectLevelId, string? userId, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllAsync(string? subjectLevelId, string? userId, int page = 1);
    }
}
