using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class RequestDAO
    {
        private readonly MyDbContext _context;
 
        private static Random random = new Random();

        public RequestDAO(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _context.Requests.ToListAsync();
        }
        public async Task<Request> GetRequestByRequestIDAsync(int requestID)
        {
            try
            {
                var Request = await _context.Requests.SingleOrDefaultAsync(t => t.Id == requestID);
                return Request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Request> GetRequestByUserIDAsync(string userID)
        {
            try
            {
                var Request = await _context.Requests.SingleOrDefaultAsync(t => t.UserId == userID);
                return Request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> SaveRequestAsync(Request request)
        {
            try
            {
                _context.Requests!.Add(request);
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return request.Id;
        }
        public async Task DeleteRequestAsync(Request request)
        {
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<Request> UpdateAsync(Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var existingRequest = await _context.Requests.FindAsync(request.Id);
            if (existingRequest == null)
            {
                throw new KeyNotFoundException("Request not found");
            }

            existingRequest.Description = request.Description;
            existingRequest.CreatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingRequest;
        }
    }
}
