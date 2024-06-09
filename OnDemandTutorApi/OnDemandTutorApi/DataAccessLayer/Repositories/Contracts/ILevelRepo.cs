using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ILevelRepo
    {
        public Task<bool> CreateAsync(Level level);
        public Task<IEnumerable<Level>> GetAllAsync();
        public Task<Level> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(Level level);
        public Task<Level> GetByNameAsync(string name);
    }
}
