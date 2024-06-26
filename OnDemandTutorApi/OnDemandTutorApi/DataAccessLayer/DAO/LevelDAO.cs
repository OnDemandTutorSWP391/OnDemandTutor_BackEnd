using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;
using Org.BouncyCastle.Asn1.Ocsp;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class LevelDAO
    {
        //private readonly MyDbContext _context;

        //public LevelDAO(MyDbContext context)
        //{
        //    _context = context;
        //}

        //CREATE
        public async Task<bool> CreateAsync(Level level)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.Levels.AddAsync(level);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                return false;
            }
        }

        //GET ALL
        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            var levels = new List<Level>();
            try
            {
                using (var context = new MyDbContext())
                {
                    levels = await context.Levels.Include(x => x.SubjectLevels).ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return levels;
        }

        //GET BY ID
        public async Task<Level> GetByIdAsync(int id)
        {
            var level = new Level();
            try
            {
                using (var context = new MyDbContext())
                {
                    level = await context.Levels.Include(x => x.SubjectLevels).SingleOrDefaultAsync(l => l.Id == id);
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return level;
        }

        //GET BY NAME
        public async Task<Level> GetByNameAsync(string name)
        {
            var level = new Level();
            try
            {
                using (var context = new MyDbContext())
                {
                    level = await context.Levels.Include(x => x.SubjectLevels).SingleOrDefaultAsync(l => l.Name == name);
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return level;
        }

        //UPDATE
        public async Task<bool> UpdateAsync(Level level)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Level>(level).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                return false;
            }
        }

        //DELETE
        public async Task<bool> DeleteAsync(Level level)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Level>(level).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                return false;
            }
        }
    }
}
