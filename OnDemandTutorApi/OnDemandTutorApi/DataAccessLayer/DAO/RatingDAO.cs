using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class RatingDAO
    {
        private readonly MyDbContext _context;

        public RatingDAO(MyDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Rating rating)
        {
            try
            {
                await _context.Ratings.AddAsync(rating);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Rating>> GetAllByTutorIdAsync(int tutorId)
        {
            var ratings = new List<Rating>();

            try
            {
                ratings = await _context.Ratings.Include(x => x.Tutor.User)
                                                .Include(x => x.User)
                                                .Where(x => x.TutorId == tutorId)
                                                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return ratings;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            var ratings = new List<Rating>();

            try
            {
                ratings = await _context.Ratings.Include(x => x.Tutor.User)
                                                .Include(x => x.User)
                                                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return ratings;
        }

        public async Task<Rating> GetByIdAsync(int ratingId)
        {
            var rating = new Rating();

            try
            {
                rating = await _context.Ratings.Include(x => x.Tutor.User)
                                                .Include(x => x.User)
                                                .SingleOrDefaultAsync(x => x.Id == ratingId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return rating;
        }

        public async Task<bool> UpdateAsync(Rating rating)
        {
            try
            {
                _context.Entry<Rating>(rating).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
               
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
