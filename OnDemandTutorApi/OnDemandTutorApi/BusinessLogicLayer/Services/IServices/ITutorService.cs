using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.IServices
{
    public interface ITutorService
    {
        public Task<int> AddTutorAsync(TutorDTO tutorDTO);
    }
}
