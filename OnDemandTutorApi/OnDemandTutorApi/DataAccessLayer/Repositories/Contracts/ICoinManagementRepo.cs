using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ICoinManagementRepo
    {
        public Task<bool> CreateCoinRecord(CoinManagement coinManagement);
        public Task<float> GetTotalCoinForUserAsync(string userId);
        public Task<IEnumerable<CoinManagement>> GetByUserIdAsync(string userId);
        public Task<IEnumerable<CoinManagement>> GetAllAsync();
    }
}
