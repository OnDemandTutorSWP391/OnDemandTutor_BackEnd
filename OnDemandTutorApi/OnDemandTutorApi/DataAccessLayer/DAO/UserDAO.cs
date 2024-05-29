using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnDemandTutorApi.DataAccessLayer.DAO
{
    public class UserDAO
    {
        private readonly MyDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserDAO(MyDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
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

        //RESET PASSWORD FOR USER
        public async Task<IdentityResult> ResetPassAsync(User resetUser, string token, string newPass)
        {
            var result = await _userManager.ResetPasswordAsync(resetUser, token, newPass);
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

        //GENERATE TOKEN
        public async Task<TokenDTO> GenerateTokenAsync(User user)
        {
            //Create access token
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id)
            };
            //
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            //
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Save access token to DB
            var userToken = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = "ODT_Api",
                Name = "AccessToken",
                Value = accessToken
            };

            //remove token(nếu có), trước khi thêm token mới
            _context.UserTokens.RemoveRange(_context.UserTokens.Where(ut => ut.UserId == user.Id && ut.LoginProvider == "ODT_Api" && ut.Name == "AccessToken"));
            await _context.AddAsync(userToken);
            await _context.SaveChangesAsync();


            //create refresh token
            var refreshToken = GenerateRefreshToken();

            //Save refresh token to DB
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1),
            };

            //remove token(nếu có), trước khi thêm token mới
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(ut => ut.UserId == user.Id));
            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        //Generate refresh token
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

    }
}
