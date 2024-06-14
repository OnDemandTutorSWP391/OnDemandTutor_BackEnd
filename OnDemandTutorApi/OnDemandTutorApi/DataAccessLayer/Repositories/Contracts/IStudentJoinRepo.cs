using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentJoinRepo
    {
        public Task<bool> CreateAsync(StudentJoin studentJoin);
        public Task<StudentJoin> GetByIdAsync(int id);
        public Task<IEnumerable<StudentJoin>> GetAllByUserIdAsync(string userId);
        public Task<IEnumerable<StudentJoin>> GetAllBySubjectLevelIdAsync(int id);
        public Task<IEnumerable<StudentJoin>> GetAllByTutorIdAsync(int id);
        public Task<IEnumerable<StudentJoin>> GetAllAsync();
        public Task<bool> DeleteAsync(StudentJoin studentJoin);
    }   
}
