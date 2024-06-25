using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class ResponseRepo : IResponseRepo
    {
        private readonly ResponseDAO _responseDAO;

        public ResponseRepo(ResponseDAO responseDAO)
        {
            _responseDAO = responseDAO;
        }
        public async Task<bool> CreateAsync(Response response)
        {
            return await _responseDAO.CreateAsync(response);
        }

        public async Task<bool> DeleteAsync(Response response)
        {
            return await _responseDAO.DeleteAsync(response);
        }

        public async Task<IEnumerable<Response>> GetAllAsync()
        {
           return await _responseDAO.GetAllAsync();
        }
    }
}
