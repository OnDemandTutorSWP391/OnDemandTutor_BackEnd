using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ICoinManagementService
    {
        public Task<ResponseDTO<CoinDTO>> DepositAsync(CoinDTO coinRequest);
        public Task<ResponseDTO<float>> GetTotalCoinForUserAsync(string userId);
        public Task<ResponseDTO<IEnumerable<CoinDTO>>> GetTransactionForUserAsync(string userId, DateTime? from, DateTime? to, string? sortBy, int page = 1);
    }
}
