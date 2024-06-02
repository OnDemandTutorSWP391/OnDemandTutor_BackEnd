using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IRequestRepo
    { 
        Task<IEnumerable<Request>> GetAllRequestsAsync();
        Task<Request> GetRequestByRequestIDAsync(int RequestID);
        Task<Request> GetRequestByUserIDAsync(string UserID);
        Task<int> AddRequestAsync(Request request);
        Task<Request> UpdateRequestAsync(Request request);
        Task DeleteRequestAsync(Request request);
    }
}