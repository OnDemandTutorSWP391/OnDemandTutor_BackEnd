using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class SubjectLevelDAO
    {
        //private readonly MyDbContext _context;

        //public SubjectLevelDAO(MyDbContext context)
        //{
        //    _context = context;
        //}

        //CREATE
        public async Task<bool> CreateAsync(SubjectLevel subjectLevel)
        {
            try
            {
                using (var context = new MyDbContext()) 
                {
                    await context.SubjectLevels.AddAsync(subjectLevel);
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
        public async Task<IEnumerable<SubjectLevel>> GetAllAsync()
        {
            var subjectLevels = new List<SubjectLevel>();
            try
            {
                using (var context = new MyDbContext())
                {
                    subjectLevels = await context.SubjectLevels.Include(x => x.Level)
                                                               .Include(x => x.Subject)
                                                               .Include(x => x.Tutor.User)
                                                               .Include(x => x.Tutor.Ratings)
                                                               .Include(x => x.StudentJoins)
                                                               .Include(x => x.Times)
                                                               .ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return subjectLevels;
        }

        //GET BY ID
        public async Task<SubjectLevel> GetByIdAsync(int id)
        {
            var subjectLevel = new SubjectLevel();
            try
            {
                using(var context = new MyDbContext())
                {
                    subjectLevel = await context.SubjectLevels.Include(x => x.Level)
                                                              .Include(x => x.Subject)
                                                              .Include(x => x.Tutor.User)
                                                              .Include(x => x.StudentJoins)
                                                              .Include(x => x.Times)
                                                              .SingleOrDefaultAsync(x => x.Id == id);
                }
                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return subjectLevel;
        }

        //UPDATE
        public async Task<bool> UpdateAsync(SubjectLevel subjectLevel)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<SubjectLevel>(subjectLevel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
        public async Task<bool> DeleteAsync(SubjectLevel subjectLevel)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<SubjectLevel>(subjectLevel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
