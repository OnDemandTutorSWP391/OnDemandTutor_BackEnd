using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IRequestCategoryRepo
    {
        public Task<bool> CreateAsync(RequestCategory requestCategory);
        public Task<IEnumerable<RequestCategory>> GetAllAsync();
        public Task<RequestCategory> GetByIdAsync(int id);
        public Task<RequestCategory> GetByNameAsync(string name);
        public Task<bool> UpdateAsync(RequestCategory requestCategory);
        public Task<bool> DeleteAsync(RequestCategory requestCategory);
    }
}
