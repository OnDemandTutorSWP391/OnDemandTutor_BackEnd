using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ISubjectLevelRepo
    {
        public Task<bool> CreateAsync(SubjectLevel subjectLevel);
        public Task<IEnumerable<SubjectLevel>> GetAllAsync();
        public Task<SubjectLevel> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(SubjectLevel subjectLevel);
        public Task<bool> DeleteAsync(SubjectLevel subjectLevel);
    }
}
