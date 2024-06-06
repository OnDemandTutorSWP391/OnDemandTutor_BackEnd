using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class RequestCategoryRepo : IRequestCategoryRepo
    {
        private readonly RequestCategoryDAO _requestCategoryDAO;

        public RequestCategoryRepo(RequestCategoryDAO requestCategoryDAO)
        {
            _requestCategoryDAO = requestCategoryDAO;
        }
        public async Task<bool> CreateAsync(RequestCategory requestCategory)
        {
            return await _requestCategoryDAO.CreateAsync(requestCategory);
        }

        public async Task<bool> DeleteAsync(RequestCategory requestCategory)
        {
            return await _requestCategoryDAO.DeleteAsync(requestCategory);
        }

        public async Task<IEnumerable<RequestCategory>> GetAllAsync()
        {
            return await _requestCategoryDAO.GetAllAsync();
        }

        public async Task<RequestCategory> GetByIdAsync(int id)
        {
            return await _requestCategoryDAO.GetByIdAsync(id);
        }

        public async Task<RequestCategory> GetByNameAsync(string name)
        {
            return await _requestCategoryDAO.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(RequestCategory requestCategory)
        {
            return await _requestCategoryDAO.UpdateAsync(requestCategory);
        }
    }
}
