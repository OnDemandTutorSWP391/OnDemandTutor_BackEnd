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
    }
}
