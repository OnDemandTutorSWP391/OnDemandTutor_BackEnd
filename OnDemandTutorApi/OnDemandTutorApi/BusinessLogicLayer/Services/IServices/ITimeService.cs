using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITimeService
    {
        public Task<ResponseApiDTO<TimeResponseDTO>> CreateAsync(TimeRequestDTO timeRequestDTO);
    }
}
