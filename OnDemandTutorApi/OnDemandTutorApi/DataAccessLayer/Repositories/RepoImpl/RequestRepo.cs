using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class RequestRepo : IRequestRepo
    {
        private readonly RequestDAO _requestDAO;
        private readonly MyDbContext _context;

        public RequestRepo(RequestDAO requestDAO, MyDbContext context) 
        {
            _requestDAO = requestDAO;
            _context = context;

        }

        public async Task<int> AddRequestAsync(Request request)
        {
            return await _requestDAO.SaveRequestAsync(request);
        }

        public async Task DeleteRequestAsync(Request request)
        {
             await _requestDAO.DeleteRequestAsync(request);
        }

        public async Task<Request> GetRequestByRequestIDAsync(int RequestID)
        {
            return await _requestDAO.GetRequestByRequestIDAsync(RequestID);
        }

        public async Task<Request> GetRequestByUserIDAsync(string UserID)
        {
            return await _requestDAO.GetRequestByUserIDAsync(UserID);
        }

        public Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return _requestDAO.GetAllRequestsAsync();
        }

        public async Task<Request> UpdateRequestAsync(Request request)
        {
            return await _requestDAO.UpdateAsync(request);
        }
    }
}