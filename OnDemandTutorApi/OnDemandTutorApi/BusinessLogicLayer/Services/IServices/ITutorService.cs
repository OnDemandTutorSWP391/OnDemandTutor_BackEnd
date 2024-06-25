using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITutorService
    {
        public Task<int> AddTutorAsync(TutorDTO tutorDTO);
        public Task<Tutor> GetTutorByIdAsync(int id);
        public Task<Tutor> GetTutorByUserIdAsync(string userId);
        public Task DeleteTutorAsync(int id);
        public Task<ResponseApiDTO> UpdateProfileAsync(string userId, ProfileRequestDTO profileTutor);
        public Task<ResponseApiDTO<ProfileRequestDTO>> GetProfileByIdAsync(int id);
        public Task<ResponseApiDTO<ProfileResponseDTO>> GetProfileAsync(string userId);
        public Task<ResponseApiDTO> UpdateStatusAsync(int id, string status);
        public Task<ResponseApiDTO<IEnumerable<TutorResponseDTO>>> GetAllTutorsForStudentAsync(string? seacrch, string? sortBy, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<TutorResponseDTO>>> GetAllTutorsAsync(string? seacrch, string? sortBy, int page = 1);
    }
}
