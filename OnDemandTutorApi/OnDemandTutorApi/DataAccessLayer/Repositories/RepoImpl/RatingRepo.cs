using AutoMapper;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class RatingRepo : IRatingRepo
    {
        private readonly RatingDAO _ratingDAO;

        public RatingRepo(RatingDAO ratingDAO)
        {
            _ratingDAO = ratingDAO;
        }
        public async Task<bool> CreateAysnc(Rating rating)
        {
            return await _ratingDAO.CreateAsync(rating);
        }

        public async Task<bool> DeleteAsync(Rating rating)
        {
            return await _ratingDAO.DeleteAsync(rating);
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _ratingDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Rating>> GetAllByTutorIdAsync(int tutorId)
        {
            return await _ratingDAO.GetAllByTutorIdAsync(tutorId);
        }

        public async Task<Rating> GetByIdAsync(int ratingId)
        {
            return await _ratingDAO.GetByIdAsync(ratingId);
        }

        public async Task<bool> UpdateAsync(Rating rating)
        {
            return await _ratingDAO.UpdateAsync(rating);
        }
    }
}
