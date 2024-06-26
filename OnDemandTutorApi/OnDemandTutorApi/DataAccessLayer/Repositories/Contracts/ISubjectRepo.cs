using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ISubjectRepo
    {
        public Task<bool> CreateAsync(Subject subject);
        public Task<IEnumerable<Subject>> GetAllAsync();
        public Task<Subject> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(Subject subject);
        public Task<bool> DeleteAsync(Subject subject);
        public Task<Subject> GetByNameAsync(string name);
    }
}
