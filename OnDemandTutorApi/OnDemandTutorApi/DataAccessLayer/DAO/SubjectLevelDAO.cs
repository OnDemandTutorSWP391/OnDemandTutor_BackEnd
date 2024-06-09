using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class SubjectLevelDAO
    {
        private readonly MyDbContext _context;

        public SubjectLevelDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(SubjectLevel subjectLevel)
        {
            try
            {
                await _context.SubjectLevels.AddAsync(subjectLevel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //GET ALL
        public async Task<IEnumerable<SubjectLevel>> GetAllAsync()
        {
            try
            {
                return await _context.SubjectLevels.Include(x => x.Level).Include(x => x.Subject).Include(x => x.Tutor.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY ID
        public async Task<SubjectLevel> GetByIdAsync(int id)
        {
            try
            {
                return await _context.SubjectLevels.Include(x => x.Level).Include(x => x.Subject).Include(x => x.Tutor.User).SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //UPDATE
        public async Task<bool> UpdateAsync(SubjectLevel subjectLevel)
        {
            try
            {
                _context.Entry<SubjectLevel>(subjectLevel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //DELETE
        public async Task<bool> DeleteAsync(SubjectLevel subjectLevel)
        {
            try
            {
                _context.Entry<SubjectLevel>(subjectLevel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
