using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class RequestCategoryDAO
    {
        private readonly MyDbContext _context;

        public RequestCategoryDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(RequestCategory category)
        {
            try
            {
                await _context.RequestCategories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET ALL
        public async Task<IEnumerable<RequestCategory>> GetAllAsync()
        {
            try
            {
                return await _context.RequestCategories.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY ID
        public async Task<RequestCategory> GetByIdAsync(int id)
        {
            try
            {
                var category = await _context.RequestCategories.SingleOrDefaultAsync(c => c.Id == id);
                return category;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //GET BY NAME
        public async Task<RequestCategory> GetByNameAsync(string name)
        {
            try
            {
                var category = await _context.RequestCategories.SingleOrDefaultAsync(c => c.CategoryName == name);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //UPDATE
        public async Task<bool> UpdateAsync(RequestCategory requestCategoryUpdate)
        {
            try
            {
                _context.Entry<RequestCategory>(requestCategoryUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //DELETE
        public async Task<bool> DeleteAsync(RequestCategory requestCategoryDelete)
        {
            try
            {
                _context.Entry<RequestCategory>(requestCategoryDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
