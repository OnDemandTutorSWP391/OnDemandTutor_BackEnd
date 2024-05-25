using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class TutorRepo : ITutorRepo
    {
        private readonly TutorDAO _tutorDAO;

        public TutorRepo(TutorDAO tutorDAO)
        {
            _tutorDAO = tutorDAO;
        }
        public async Task<int> AddTutorAsync(Tutor tutor)
        {
            return await _tutorDAO.SaveTutorAsync(tutor);
        }

        public async Task<Tutor> GetByIdAsync(int id)
        {
            return await _tutorDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Tutor>> GetTutorsAsync()
        {
            return await _tutorDAO.GetTutorsAsync();
        }
    }
}
