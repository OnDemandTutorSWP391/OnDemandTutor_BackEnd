using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IStudentJoinService
    {
        public Task<ResponseApiDTO<StudentJoinResponseDTO>> CreateAsync(StudentJoinRequestDTO studentJoinDTO);
        public Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllAsync(string? subjectLevelId, string? userId, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllByStudentIdAsync(string studentId, string? subjectLevelId, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<StudentJoinResponseDTO>>> GetAllByTutorIdAsync(string userId, string? subjectLevelId, string? studentlId, int page = 1);
        public Task<ResponseApiDTO> DeleteForTutorAsync(int studentJoinId);
        public Task<ResponseApiDTO> DeleteForStudentAsync(int studentJoinId);
        public Task<ResponseApiDTO> DeleteForStaffAsync(int studentJoinId);
    }
}
