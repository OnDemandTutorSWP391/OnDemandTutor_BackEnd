using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ITimeRepo
    {
        public Task<bool> CreateAsync(Time time);
        public Task<Time> GetByIdAsync(int id);
        public Task<Time> GetByDateAsync(DateTime startSlot, DateTime endSlot, DateTime date);
        public Task<IEnumerable<Time>> GetAllByStudentIdAsync(string studentId);
        public Task<IEnumerable<Time>> GetAllByTutorIdAsync(int tutorId);
    }
}
