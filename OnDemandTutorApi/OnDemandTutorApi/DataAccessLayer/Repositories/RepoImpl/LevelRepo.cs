using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class LevelRepo : ILevelRepo
    {
        private readonly LevelDAO _levelDAO;

        public LevelRepo(LevelDAO levelDAO)
        {
            _levelDAO = levelDAO;
        }
        public async Task<bool> CreateAsync(Level level)
        {
            return await _levelDAO.CreateAsync(level);
        }

        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            return await _levelDAO.GetAllAsync();
        }

        public async Task<Level> GetByIdAsync(int id)
        {
            return await _levelDAO.GetByIdAsync(id);
        }

        public async Task<Level> GetByNameAsync(string name)
        {
            return await _levelDAO.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(Level level)
        {
            return await(_levelDAO.UpdateAsync(level));
        }
    }
}
