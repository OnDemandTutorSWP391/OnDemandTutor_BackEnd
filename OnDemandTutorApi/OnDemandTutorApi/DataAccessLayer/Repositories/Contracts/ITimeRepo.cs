using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ITimeRepo
    {
        public Task<bool> CreateAsync(Time time);
    }
}
