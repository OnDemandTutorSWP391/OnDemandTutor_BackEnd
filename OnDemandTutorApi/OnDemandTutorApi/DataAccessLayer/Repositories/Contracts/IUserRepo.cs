using Microsoft.AspNetCore.Identity;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.Contracts
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User updatedUser);
        Task<IdentityResult> DeleteUserAsync(User deletedUser);
        Task<User> GetUserByEmailAsync(string email);
    }
}
