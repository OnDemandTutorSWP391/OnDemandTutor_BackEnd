using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class TimeRepo : ITimeRepo
    {
        private readonly TimeDAO _timeDAO;

        public TimeRepo(TimeDAO timeDAO)
        {
            _timeDAO = timeDAO;
        }
        public async Task<bool> CreateAsync(Time time)
        {
            return await _timeDAO.CreateAsync(time);
        }

        public async Task<IEnumerable<Time>> GetAllAsycn()
        {
            return await _timeDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Time>> GetAllByUserIdAsync(string studentId)
        {
            return await _timeDAO.GetAllByUserIdAsync(studentId);
        }

        public async Task<Time> GetByDateAsync(DateTime startSlot, DateTime endSlot, DateTime date)
        {
            return await _timeDAO.GetByDateAsync(startSlot, endSlot, date); 
        }

        public async Task<Time> GetByIdAsync(int id)
        {
            return await _timeDAO.GetByIdAsync(id);
        }
    }
}
