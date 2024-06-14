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

        public async Task<IEnumerable<Time>> GetAllAsync()
        {
            return await _timeDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Time>> GetAllByStudentIdAsync(string studentId)
        {
            return await _timeDAO.GetAllByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<Time>> GetAllByTutorIdAsync(int tutorId)
        {
            return await _timeDAO.GetAllByTutorIdAsync(tutorId);
        }

        public async Task<Time> GetByDateAsync(DateTime startSlot, DateTime endSlot, DateTime date)
        {
            return await _timeDAO.GetByDateAsync(startSlot, endSlot, date); 
        }

        public async Task<Time> GetByIdAsync(int id)
        {
            return await _timeDAO.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Time time)
        {
            return await _timeDAO.UpdateAsync(time);
        }
    }
}
