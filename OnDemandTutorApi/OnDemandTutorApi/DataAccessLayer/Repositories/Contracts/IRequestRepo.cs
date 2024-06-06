using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IRequestRepo
    {
        public Task<bool> CreateAsync(Request request);
        public Task<IEnumerable<Request>> GetAllByUserIdAsync(string userId);
        public Task<IEnumerable<Request>> GetAllAsync();
        public Task<bool> UpdateAsync(Request requestUpdate);
        public Task<Request> GetByIdAsync(int id);
    }
}
