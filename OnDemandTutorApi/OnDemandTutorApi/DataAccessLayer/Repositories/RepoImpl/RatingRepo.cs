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
    }
}
