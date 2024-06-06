using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class RequestRepo : IRequestRepo
    {
        private readonly RequestDAO _requestDAO;

        public RequestRepo(RequestDAO requestDAO)
        {
            _requestDAO = requestDAO;
        }
        public async Task<bool> CreateAsync(Request request)
        {
            return await _requestDAO.CreateAsync(request); 
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _requestDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Request>> GetAllByUserIdAsync(string userId)
        {
            return await _requestDAO.GetAllByUserIdAsync(userId);
        }

        public async Task<Request> GetByIdAsync(int id)
        {
            return await _requestDAO.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Request requestUpdate)
        {
            return await _requestDAO.UpdateAsync(requestUpdate);
        }
    }
}
