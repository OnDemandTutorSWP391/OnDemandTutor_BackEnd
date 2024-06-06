using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;
using Org.BouncyCastle.Asn1.Ocsp;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class ResponseDAO
    {
        private readonly MyDbContext _context;

        public ResponseDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(Response response)
        {
            try
            {
                await _context.Responses.AddAsync(response);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET ALL
        public async Task<IEnumerable<Response>> GetAllAsync()
        {
            try
            {
                return await _context.Responses.Include(x => x.Request.Category).Include(x => x.Request.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
