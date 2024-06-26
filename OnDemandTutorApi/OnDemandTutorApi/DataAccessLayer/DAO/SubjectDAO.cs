using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class SubjectDAO
    {
        //private readonly MyDbContext _context;

        //public SubjectDAO(MyDbContext context)
        //{
        //    _context = context;
        //}

        //CREATE
        public async Task<bool> CreateAsync(Subject subject)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.Subjects.AddAsync(subject);
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
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var subjects = new List<Subject>();
            try
            {
                using (var context = new MyDbContext())
                {
                    subjects = await context.Subjects.Include(x => x.SubjectLevels).ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return subjects;
        }

        //GET BY ID
        public async Task<Subject> GetByIdAsync(int id)
        {
            var subject = new Subject();
            try
            {
                using (var context = new MyDbContext())
                {
                    subject = await context.Subjects.Include(x => x.SubjectLevels).SingleOrDefaultAsync(s => s.Id == id);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return subject;
        }

        //GET BY NAME
        public async Task<Subject> GetByNameAsync(string name)
        {
            var subject = new Subject();
            try
            {
                using (var context = new MyDbContext())
                {
                    subject = await context.Subjects.Include(x => x.SubjectLevels).SingleOrDefaultAsync(s => s.Name == name);
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return subject;
        }

        //UPDATE
        public async Task<bool> UpdateAsync(Subject subject)
        {
            try
            {
                using(var context = new MyDbContext())
                {
                    context.Entry<Subject>(subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
        public async Task<bool> DeleteAsync(Subject subject)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Subject>(subject).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
