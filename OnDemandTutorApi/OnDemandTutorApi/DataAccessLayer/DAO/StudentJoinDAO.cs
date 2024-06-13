﻿using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class StudentJoinDAO
    {
        //private readonly MyDbContext _context;

        //public StudentJoinDAO(MyDbContext context)
        //{
        //    _context = context;
        //}

        //CREATE
        public async Task<bool> CreateAsync(StudentJoin studentJoin)
        {
            try
            {
                using(var context = new MyDbContext())
                {
                    await context.StudentJoins.AddAsync(studentJoin);
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

        //GET BY ID
        public async Task<StudentJoin> GetByIdAsync(int id)
        {
            var studentJoin = new StudentJoin();
            try
            {
                using(var context = new MyDbContext())
                {
                    studentJoin = await context.StudentJoins.Include(x => x.SubjectLevel.Tutor.User)
                                                            .Include(x => x.User)
                                                            .SingleOrDefaultAsync(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return studentJoin;
        }

        //GET ALL BY SUBJECT LEVEL Id
        public async Task<IEnumerable<StudentJoin>> GetBySubjectLevelIdAsync(int id)
        {
            var studentJoins = new List<StudentJoin>();
            try
            {
                using (var context = new MyDbContext())
                {
                    studentJoins = await context.StudentJoins.Include(x => x.SubjectLevel.Tutor.User)
                                                             .Include(x => x.User)
                                                             .Where(x => x.SubjectLevelId == id)
                                                             .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return studentJoins;
        }

        //GET BY USER ID
        public async Task<StudentJoin> GetByUserIdAsync(string userId)
        {
            var studentJoin = new StudentJoin();
            try
            {
                using (var context = new MyDbContext())
                {
                    studentJoin = await context.StudentJoins.Include(x => x.SubjectLevel.Tutor.User)
                                                            .Include(x => x.User)
                                                            .SingleOrDefaultAsync(x => x.UserId == userId);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return studentJoin;
        }

        //GET ALL
        public async Task<IEnumerable<StudentJoin>> GetAllAsync()
        {
            var studentJoins = new List<StudentJoin>();
            try
            {
                using (var context = new MyDbContext())
                {
                    studentJoins = await context.StudentJoins.Include(x => x.SubjectLevel.Tutor.User)
                                                             .Include(x => x.User)
                                                             .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return studentJoins;
        }

        //DELETE
        public async Task<bool> DeleteAysnc(StudentJoin studentJoin)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<StudentJoin>(studentJoin).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
