using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface IRatingService
    {
        public Task<ResponseApiDTO<RatingResponseDTO>> CreateAsync(string userId, RatingRequestDTO ratingRequestDTO);
    }
}
