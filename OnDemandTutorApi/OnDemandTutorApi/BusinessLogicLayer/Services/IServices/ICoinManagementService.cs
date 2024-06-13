using OnDemandTutorApi.BusinessLogicLayer.DTO;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ICoinManagementService
    {
        public Task<ResponseApiDTO<CoinResponseDTO>> DepositAsync(CoinDTO coinRequest);
        public Task<ResponseApiDTO<CoinTransferResponseDTO>> TransferAsync(string userId, string receiverId, float coin);
        public Task<ResponseApiDTO<float>> GetTotalCoinForUserAsync(string userId);
        public Task<ResponseApiDTO<IEnumerable<CoinResponseDTO>>> GetTransactionForUserAsync(string userId, DateTime? from, DateTime? to, string? sortBy, int page = 1);
    }
}
