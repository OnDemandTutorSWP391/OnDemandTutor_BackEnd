using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class SubjectDAO
    {
        private readonly MyDbContext _context;

        public SubjectDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(Subject subject)
        {
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //GET ALL
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            try
            {
                return await _context.Subjects.Include(x => x.SubjectLevels).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY ID
        public async Task<Subject> GetByIdAsync(int id)
        {
            try
            {
                var subject = await _context.Subjects.SingleOrDefaultAsync(s => s.Id == id);
                return subject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY NAME
        public async Task<Subject> GetByNameAsync(string name)
        {
            try
            {
                var subject = await _context.Subjects.SingleOrDefaultAsync(s => s.Name == name);
                return subject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //UPDATE
        public async Task<bool> UpdateAsync(Subject subject)
        {
            try
            {
                _context.Entry<Subject>(subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
