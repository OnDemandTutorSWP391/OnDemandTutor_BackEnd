using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface ITutorRepo
    {
        Task<IEnumerable<Tutor>> GetTutorsAsync();
        Task<int> AddTutorAsync(Tutor tutor);
        Task<Tutor> GetByIdAsync(int id);
    }
}
