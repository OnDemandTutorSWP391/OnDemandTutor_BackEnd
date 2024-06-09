using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;
using Org.BouncyCastle.Asn1.Ocsp;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class LevelDAO
    {
        private readonly MyDbContext _context;

        public LevelDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(Level level)
        {
            try
            {
                await _context.Levels.AddAsync(level);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //GET ALL
        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            try
            {
                return await _context.Levels.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY ID
        public async Task<Level> GetByIdAsync(int id)
        {
            try
            {
                var level = await _context.Levels.SingleOrDefaultAsync(l => l.Id == id);
                return level;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY NAME
        public async Task<Level> GetByNameAsync(string name)
        {
            try
            {
                var level = await _context.Levels.SingleOrDefaultAsync(c => c.Name == name);
                return level;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //UPDATE
        public async Task<bool> UpdateAsync(Level level)
        {
            try
            {
                _context.Entry<Level>(level).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
