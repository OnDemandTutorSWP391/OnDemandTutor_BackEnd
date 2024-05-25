using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITutorRepo _tutorRepo;
        private readonly MyDbContext _context;

        public UserService(IUserRepo userRepo, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration, ITutorRepo tutorRepo, MyDbContext context)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _tutorRepo = tutorRepo;
            _context = context;
        }

        public async Task<UserProfileUpdateDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserProfileUpdateDTO>(user);
        }

        public async Task<string> SignInAsync(UserAuthenDTO userAuthen)
        {
            var user = await _userRepo.GetUserByEmailAsync(userAuthen.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(user, userAuthen.Password);

            if (user == null || !passwordValid)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userAuthen.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(UserRequestDTO userRequestDTO)
        {
            //check role valid
            var validRoles = new List<string> { RoleDTO.Tutor, RoleDTO.Student };
            if (!validRoles.Contains(userRequestDTO.Role))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid role. Choose either Tutor or Student." });
            }
            // Map UserDTORequest to User entity
            var user = _mapper.Map<User>(userRequestDTO);

            user.UserName = userRequestDTO.Email;

            // Call repository to save the user
            var result = await _userRepo.AddUserAsync(user, userRequestDTO.Password);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                // Ensure the role exists
                if (!await _roleManager.RoleExistsAsync(userRequestDTO.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(userRequestDTO.Role));
                    await _context.SaveChangesAsync();
                }

                // Add user to the role
                await _userManager.AddToRoleAsync(user, userRequestDTO.Role);
                await _context.SaveChangesAsync();

                //check if role == Tutor
                if (userRequestDTO.Role.Equals(RoleDTO.Tutor))
                {
                    var tutorDTO = new TutorDTO();
                    tutorDTO.UserId = user.Id;
                    var tutor = _mapper.Map<Tutor>(tutorDTO);
                    await _tutorRepo.AddTutorAsync(tutor);
                }
            }

            return result;

        }


        public async Task<IdentityResult> UpdateProfileUserAsync(UserProfileUpdateDTO userDTO, string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            _mapper.Map(userDTO, user);

            return await _userRepo.UpdateUserAsync(user);
        }
    }
}
