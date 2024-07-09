using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IRatingService
    {
        public Task<ResponseApiDTO<RatingResponseDTO>> CreateAsync(string userId, RatingRequestDTO ratingRequestDTO);
        public Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllByTutorIdAsync(int tutorId, string? sortBy, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllByTutorSelfAsync(string userId, string? sortBy, int page = 1);
        public Task<ResponseApiDTO<IEnumerable<RatingResponseDTO>>> GetAllAsync(string? userId, int tutorId, string? sortBy, int page = 1);
        public Task<ResponseApiDTO> UpdateAsync(int ratingId, string userId, RatingUpdateDTO ratingUpdateDTO);
        public Task<ResponseApiDTO> DeleteForStudentAsync(int ratingId, string userId);
        public Task<ResponseApiDTO> DeleteAsync(int ratingId);
    }
}
