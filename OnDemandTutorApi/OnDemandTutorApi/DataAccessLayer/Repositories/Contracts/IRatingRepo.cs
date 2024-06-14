using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IRatingRepo
    {
        public Task<bool> CreateAysnc(Rating rating);
        public Task<IEnumerable<Rating>> GetAllByTutorIdAsync(int tutorId);
        public Task<IEnumerable<Rating>> GetAllAsync();
        public Task<Rating> GetByIdAsync(int ratingId);
        public Task<bool> UpdateAsync(Rating rating);
    }
}
