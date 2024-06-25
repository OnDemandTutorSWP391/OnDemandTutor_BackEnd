using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IResponseRepo
    {
        public Task<bool> CreateAsync(Response response);
        public Task<IEnumerable<Response>> GetAllAsync();
        public Task<bool> DeleteAsync(Response response);
    }
}
