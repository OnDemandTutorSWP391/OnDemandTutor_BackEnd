using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class SubjectLevelRepo : ISubjectLevelRepo
    {
        private readonly SubjectLevelDAO _subjectLevelDAO;

        public SubjectLevelRepo(SubjectLevelDAO subjectLevelDAO)
        {
            _subjectLevelDAO = subjectLevelDAO;
        }
        public async Task<bool> CreateAsync(SubjectLevel subjectLevel)
        {
            return await _subjectLevelDAO.CreateAsync(subjectLevel);
        }

        public async Task<bool> DeleteAsync(SubjectLevel subjectLevel)
        {
            return await _subjectLevelDAO.DeleteAsync(subjectLevel);
        }

        public async Task<IEnumerable<SubjectLevel>> GetAllAsync()
        {
            return await _subjectLevelDAO.GetAllAsync();
        }

        public async Task<SubjectLevel> GetByIdAsync(int id)
        {
            return await _subjectLevelDAO.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(SubjectLevel subjectLevel)
        {
            return await _subjectLevelDAO.UpdateAsync(subjectLevel);
        }
    }
}
