using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class SubjectRepo : ISubjectRepo
    {
        private readonly SubjectDAO _subjectDAO;

        public SubjectRepo(SubjectDAO subjectDAO)
        {
            _subjectDAO = subjectDAO;
        }
        public async Task<bool> CreateAsync(Subject subject)
        {
            return await _subjectDAO.CreateAsync(subject);
        }

        public async Task<bool> DeleteAsync(Subject subject)
        {
            return await _subjectDAO.DeleteAsync(subject);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _subjectDAO.GetAllAsync();
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            return await _subjectDAO.GetByIdAsync(id);
        }

        public async Task<Subject> GetByNameAsync(string name)
        {
            return await _subjectDAO.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(Subject subject)
        {
            return await (_subjectDAO.UpdateAsync(subject));
        }
    }
}
