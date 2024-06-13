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

        //GET BY ID
        public async Task<Time> GetByIdAsync(int id)
        {
            var time = new Time();
            try
            {
                time = await _context.Times.Include(x => x.SubjectLevel).SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return time;
        }

        //GET BY DATE
        public async Task<Time> GetByDateAsync(DateTime startSlot, DateTime endSlot, DateTime date)
        {
            var time = new Time();
            try
            {
                time = await _context.Times
                             .Include(x => x.SubjectLevel)
                             .SingleOrDefaultAsync(x => x.StartSlot == startSlot || x.EndSlot == endSlot || x.Date == date);

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return time;
        }


        //GET ALL
        public async Task<IEnumerable<Time>> GetAllAsync()
        {
            var times = new List<Time>();
            try
            {
                times = await _context.Times.Include(x => x.SubjectLevel).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return times;
        }

        //GET ALL BY StudentID 
        public async Task<IEnumerable<Time>> GetAllByStudentIdAsync(string studentId)
        {
            var times = new List<Time>();

            try
            {
                times = await (from time in _context.Times.Include(x => x.SubjectLevel)
                               join subjectLevel in _context.SubjectLevels on time.SubjectLevelId equals subjectLevel.Id
                               join studentJoin in _context.StudentJoins on subjectLevel.Id equals studentJoin.SubjectLevelId
                               where studentJoin.UserId == studentId
                               select time).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return times;
        }

        //GET ALL BY TutorID 
        public async Task<IEnumerable<Time>> GetAllByTutorIdAsync(int tutorId)
        {
            var times = new List<Time>();

            try
            {
                times = await (from time in _context.Times.Include(x => x.SubjectLevel)
                               join subjectLevel in _context.SubjectLevels on time.SubjectLevelId equals subjectLevel.Id
                               where subjectLevel.TutorId == tutorId
                               select time).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return times;
        }

    }
}
