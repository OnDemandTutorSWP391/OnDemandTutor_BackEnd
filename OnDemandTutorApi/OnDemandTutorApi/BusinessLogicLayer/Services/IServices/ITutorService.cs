using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITutorService
    {
        public Task<int> AddTutorAsync(TutorDTO tutorDTO);
        public Task<Tutor> GetTutorByIdAsync(int id);
        public Task<Tutor> GetTutorByUserIdAsync(string userId);
        public Task DeleteTutorAsync(int id);
    }
}
