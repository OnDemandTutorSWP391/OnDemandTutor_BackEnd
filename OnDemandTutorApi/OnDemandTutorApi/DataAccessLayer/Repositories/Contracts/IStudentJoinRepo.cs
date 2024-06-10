using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentJoinRepo
    {
        public Task<bool> CreateAsync(StudentJoin studentJoin);
        public Task<StudentJoin> GetByIdAsync(int id);
    }   
}
