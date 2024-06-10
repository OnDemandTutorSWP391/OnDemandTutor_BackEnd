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
    }
}
