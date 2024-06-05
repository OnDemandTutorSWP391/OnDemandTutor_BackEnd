using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class TutorDAO
    {
        private readonly MyDbContext _context;
        private static Random random = new Random();

        public TutorDAO(MyDbContext context)
        {
            _context = context;
        }

        //GET ALL TUTOR
        public async Task<IEnumerable<Tutor>> GetTutorsAsync()
        {
            return await _context.Tutors.ToListAsync();

        }

        //GET TUTOR BY TUTOR ID
        public async Task<Tutor> GetByIdAsync(int id)
        {
            try
            {
                var tutor = await _context.Tutors.SingleOrDefaultAsync(t => t.TutorId == id);
                return tutor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        //Create Tutor
        public async Task<int> SaveTutorAsync(Tutor tutor)
        {
           
            try
            {
                _context.Tutors!.Add(tutor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tutor.TutorId;
        }

        //GET TUTOR BY USERID
        public async Task<Tutor?> GetTutorByUserIdAsync(string userId)
        {
            return await _context.Tutors.SingleOrDefaultAsync(t => t.UserId == userId);
        }

        //DELETE TUTOR
        public async Task DeleteTutorAsync(Tutor tutor)
        {
            _context.Tutors.Remove(tutor);
            await _context.SaveChangesAsync();
        }

        internal static string GenerateTutorId()
        {
            const string chars = "abcdefghijklmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
