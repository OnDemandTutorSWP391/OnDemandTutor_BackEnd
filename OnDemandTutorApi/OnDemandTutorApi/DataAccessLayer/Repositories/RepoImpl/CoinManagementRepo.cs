using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class CoinManagementRepo : ICoinManagementRepo
    {
        private readonly CoinManagementDAO _coinManagementDAO;

        public CoinManagementRepo(CoinManagementDAO coinManagementDAO)
        {
            _coinManagementDAO = coinManagementDAO;
        }

        public async Task<bool> CreateCoinRecord(CoinManagement coinManagement)
        {
             return await _coinManagementDAO.CreateAsync(coinManagement);
        }

        public async Task<float> GetTotalCoinForUserAsync(string userId)
        {
            return await _coinManagementDAO.GetTotalCoinForUserAsync(userId);
        }

        public async Task<IEnumerable<CoinManagement>> GetByUserIdAsync(string userId)
        {
            return await _coinManagementDAO.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<CoinManagement>> GetAllAsync()
        {
            return await _coinManagementDAO.GetAllAsync();
        }
    }
}
