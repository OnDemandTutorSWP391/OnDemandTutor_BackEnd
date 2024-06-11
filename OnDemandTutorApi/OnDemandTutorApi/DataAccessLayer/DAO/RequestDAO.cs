using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class RequestDAO
    {
        private readonly MyDbContext _context;

        public RequestDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(Request request)
        {
            try
            {
                await _context.Requests.AddAsync(request);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //GET ALL BY USER ID
        public async Task<IEnumerable<Request>> GetAllByUserIdAsync(string userId)
        {
            try
            {
                return await _context.Requests.Include(x => x.User).Include(x => x.Category).Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET ALL
        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            try
            {
                return await _context.Requests.Include(x => x.User).Include(x => x.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY ID
        public async Task<Request> GetByIdAsync(int id)
        {
            try
            {
                var request = await _context.Requests.Include(x => x.User).Include(x => x.Category).SingleOrDefaultAsync(r => r.Id == id);
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //UPDATE
        public async Task<bool> UpdateAsync(Request requestUpdate)
        {
            try
            {
                _context.Entry<Request>(requestUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
