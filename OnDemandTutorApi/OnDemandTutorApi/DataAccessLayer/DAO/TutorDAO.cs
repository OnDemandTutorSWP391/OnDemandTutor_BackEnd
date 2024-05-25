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
            Tutor? tutor = new Tutor();
            try
            {
                tutor = await _context.Tutors.SingleOrDefaultAsync(t => t.TutorId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tutor;
        }

        //SAVE Tutor
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

        internal static string GenerateTutorId()
        {
            const string chars = "abcdefghijklmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
