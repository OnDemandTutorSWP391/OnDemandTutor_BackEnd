using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IRatingRepo
    {
        public Task<bool> CreateAysnc(Rating rating);
    }
}
