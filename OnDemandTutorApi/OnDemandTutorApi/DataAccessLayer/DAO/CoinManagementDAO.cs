using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class CoinManagementDAO
    {
        private readonly MyDbContext _context;

        public CoinManagementDAO(MyDbContext context)
        {
            _context = context;
        }

        //Add
        public async Task CreateAsync(CoinManagement coinManagement)
        {
            await _context.CoinManagements.AddAsync(coinManagement);
            await _context.SaveChangesAsync();
        }

        //Get total coin for user
        public async Task<float> GetTotalCoinForUserAsync(string userId)
        {
            var coins = await _context.CoinManagements
                                      .Where(x => x.UserId == userId)
                                      .ToListAsync();

            var total = coins.Sum(x => x.Coin);
            return total;
        }

        //get record by user id
        public async Task<IEnumerable<CoinManagement>> GetByUserIdAsync(string userId)
        {
            var records = await _context.CoinManagements
                                      .Where(x => x.UserId == userId)
                                      .ToListAsync();
            return records;
        }

        public async Task<IEnumerable<CoinManagement>> GetAllAsync() 
        {
            return await _context.CoinManagements.ToListAsync();
        }
    }
}
