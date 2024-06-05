using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl
{
    public class UserRepo : IUserRepo
    {
        private readonly UserDAO _userDAO;
        private readonly MyDbContext _context;

        public UserRepo(UserDAO userDAO, MyDbContext context) 
        {
            _userDAO = userDAO;
            _context = context;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userDAO.SaveUserAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUserAsync(User deletedUser)
        {
            return await _userDAO.DeleteUserAsync(deletedUser);
        }

        public async Task<TokenDTO> GenerateTokenAsync(User user)
        {
            return await _userDAO.GenerateTokenAsync(user);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _userDAO.GetUserByEmailAndPasswordAsync(email, password);
        }

        public async Task<User> GetByIdAsync(string id)
        {
           return await _userDAO.GetByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userDAO.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
           return await _userDAO.GetUsersAsync();
        }

        public async Task<IdentityResult> ResetUserPass(User resetUser, string token, string newPass)
        {
            return await _userDAO.ResetPassAsync(resetUser, token, newPass);
        }

        public async Task<IdentityResult> UpdateUserAsync(User updatedUser)
        {
           return await _userDAO.UpdateUserAsync(updatedUser);
        }
    }
}
