﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
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

        public async Task<ResponseDTO<TokenDTO>> RenewTokenAsync(TokenDTO tokenDTO)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var tokenValidateParam = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false //ko kiểm tra token hết hạn
            };

            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenDTO.AccessToken, tokenValidateParam, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    Console.WriteLine("Algorithm: " + jwtSecurityToken.Header.Alg);
                    Console.WriteLine(SecurityAlgorithms.HmacSha512);
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        return new ResponseDTO<TokenDTO>
                        {
                            Success = false,
                            Message = "Invalid token."
                        };
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Access token has not yet expired."
                    };
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenDTO.RefreshToken);
                if (storedToken == null)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Refresh token does not exist."
                    };
                }

                //check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Refresh token has been used."
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Refresh token has been revoked."
                    };
                }

                //check 6: AccessToken id == JwtId in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Token doesn't match."
                    };
                }

                //create new token
                var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == storedToken.UserId);
                var token = await _userRepo.GenerateTokenAsync(user);

                return new ResponseDTO<TokenDTO>
                {
                    Success = true,
                    Message = "Renew token success.",
                    Data = token
                };
            }
            catch (Exception ex) 
            {
                return new ResponseDTO<TokenDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseDTO> ResetPassAsync(UserResetPassDTO userReset)
        {
            var user = await _userRepo.GetUserByEmailAsync(userReset.Email);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Email not found!!!"
                };
            }

            var result = await _userRepo.ResetUserPass(user, userReset.Token, userReset.NewPassword);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Changes password failed, please check your email again!!! \nPassword phai co chu hoa, chu thuong, va ki tu dac biet."
                };
            }

            return new ResponseDTO
            {
                Success = true,
                Message = "Your password has been changed"
            };
        }

        public async Task<ResponseDTO<TokenDTO>> SignInAsync(UserAuthenDTO userAuthen)
        {
            var adminEmail = _configuration["Admin:Email"];
            
            if(userAuthen.Email.Equals(adminEmail))
            {
                var admin = await _userRepo.GetUserByEmailAsync(userAuthen.Email);
                var adminPasswordValid = await _userManager.CheckPasswordAsync(admin, userAuthen.Password);

                if (admin == null || !adminPasswordValid)
                {
                    return new ResponseDTO<TokenDTO>
                    {
                        Success = false,
                        Message = "Invalid password for admin."
                    };
                }

                var adminToken = await _userRepo.GenerateTokenAsync(admin);

                return new ResponseDTO<TokenDTO>
                {
                    Success = true,
                    Message = "Welcome to admin account.",
                    Data = adminToken
                };
            }

            var user = await _userRepo.GetUserByEmailAsync(userAuthen.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(user, userAuthen.Password);

            if (user == null || !passwordValid)
            {
                return new ResponseDTO<TokenDTO>
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var token = await _userRepo.GenerateTokenAsync(user);

            return new ResponseDTO<TokenDTO>
            {
                Success = true,
                Message = "Authenticate succesfull.",
                Data = token
            };
        }

        public async Task<ResponseDTO> SignUpAsync(UserRequestDTO userRequestDTO)
        {
            //check User exist
            var existedUser = await _userManager.FindByEmailAsync(userRequestDTO.Email);
            if (existedUser != null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Email already existed.",
                };
            }

            //check role valid
            var validRoles = new List<string> { RoleDTO.Tutor, RoleDTO.Student };
            if (!validRoles.Contains(userRequestDTO.Role))
            {
                return new ResponseDTO 
                {
                    Success = false,
                    Message = "Invalid Role. Choose either Tutor and Student.",
                };
            }
            // Map UserDTORequest to User entity
            var user = _mapper.Map<User>(userRequestDTO);
            user.UserName = userRequestDTO.Email;

            //Check role exist ?
            if(await _roleManager.RoleExistsAsync(userRequestDTO.Role))
            {
                var result = await _userRepo.AddUserAsync(user, userRequestDTO.Password);
                await _context.SaveChangesAsync();
                if(!result.Succeeded)
                {
                    return new ResponseDTO
                    {
                        Success = false,
                        Message = "User failed to create. \nPlease check your information. \nPassword phai co chu hoa, chu thuong, va ki tu dac biet."
                    };
                }

                //Add role to the user here...
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

                return new ResponseDTO
                {
                    Success = true,
                    Message = "Sign up successfully."
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "This role does not exist."
                };
            }

        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}
