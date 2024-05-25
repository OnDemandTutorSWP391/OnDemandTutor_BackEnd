using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class UserDAO
    {
        private readonly MyDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserDAO(MyDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //GET ALL USER
        public async Task<IEnumerable<User>> GetUsersAsync() 
        {
            return await _userManager.Users.ToListAsync();
        }

        //GET USER BY ID
        public async Task<User> GetByIdAsync(string id)
        {
           return await _userManager.FindByIdAsync(id);
        }

        //GET USER BY EMAIL AND PASSWORD
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                return null;
            }

            return user;
        }

        //SAVE USER
        public async Task<IdentityResult> SaveUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        //UPDATE USER
        public async Task<IdentityResult> UpdateUserAsync(User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id);
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            // Update the user entity with the updated values
            user = updatedUser;

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        //DELETE USER
        public async Task<IdentityResult> DeleteUserAsync(User deletedUser)
        {
            var user = await _userManager.FindByIdAsync(deletedUser.Id);
            if (user == null)
            {
                return IdentityResult.Failed();
            }
            return await _userManager.DeleteAsync(user);
        }

        //GET USER BY EMAIL
        internal async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email); 
            return user;
        }


        
    }
}
