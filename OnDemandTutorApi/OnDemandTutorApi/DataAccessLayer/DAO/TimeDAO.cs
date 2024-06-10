using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class TimeDAO
    {
        private readonly MyDbContext _context;

        public TimeDAO(MyDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task<bool> CreateAsync(Time time)
        {
            try
            {
                await _context.Times.AddAsync(time);
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
