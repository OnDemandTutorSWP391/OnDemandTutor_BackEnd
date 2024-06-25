﻿using AutoMapper;
using Humanizer;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using System.Drawing.Printing;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class AdminService : IAdminService
    {

        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DataAccessLayer.Entity.User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITutorService _tutorService;
        private readonly MyDbContext _context;
        private readonly DataAccessLayer.Repositories.Contracts.ICoinManagementRepo _coinManagementRepo;

        public static int PAGE_SIZE { get; set; } = 5;

        public AdminService(IUserRepo userRepo, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<DataAccessLayer.Entity.User> userManager, IConfiguration configuration, 
            ITutorService tutorService, MyDbContext context, DataAccessLayer.Repositories.Contracts.ICoinManagementRepo coinManagementRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _tutorService = tutorService;
            _context = context;
            _coinManagementRepo = coinManagementRepo;
        }

        public async Task<ResponseApiDTO> CreateUserAsync(UserRequestDTO userRequest)
        {
            //check User exist
            var existedUser = await _userManager.FindByEmailAsync(userRequest.Email);
            if (existedUser != null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Email already existed.",
                };
            }

            // Map UserDTORequest to User entity
            var user = _mapper.Map<DataAccessLayer.Entity.User>(userRequest);
            user.UserName = userRequest.Email;

            //Check role exist ?
            if (await _roleManager.RoleExistsAsync(userRequest.Role))
            {
                var result = await _userRepo.AddUserAsync(user, userRequest.Password);
                await _context.SaveChangesAsync();
                if (!result.Succeeded)
                {
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = "User failed to create. \nPlease check your information. \nPassword phai co chu hoa, chu thuong, va ki tu dac biet."
                    };
                }

                //Add role to the user here...
                await _userManager.AddToRoleAsync(user, userRequest.Role);
                await _context.SaveChangesAsync();

                //check if role == Tutor
                if (userRequest.Role.Equals(RoleDTO.Tutor))
                {
                    var tutorDTO = new TutorDTO();
                    tutorDTO.UserId = user.Id;
                    await _tutorService.AddTutorAsync(tutorDTO);
                }

                return new ResponseApiDTO
                {
                    Success = true,
                    Message = "Created user successfully."
                };
            }
            else
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "This role does not exist."
                };
            }
        }
        public async Task<ResponseApiDTO> DeleteUserAsync(string id)
        {
            var deletedUser = await _userRepo.GetByIdAsync(id);
            if (deletedUser == null) 
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var deletedUserRoles = await _userManager.GetRolesAsync(deletedUser);

            if (deletedUserRoles.Contains(RoleDTO.Tutor))
            {
                var tutor = await _tutorService.GetTutorByUserIdAsync(deletedUser.Id);
                await _tutorService.DeleteTutorAsync(tutor.Id);
            }

            await _userManager.RemoveFromRolesAsync(deletedUser, deletedUserRoles);
            await _context.SaveChangesAsync();

            var result = await _userRepo.DeleteUserAsync(deletedUser);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Delete user failed. \nPlease try again"
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Delete user succesfully."
            };
        }
        public async Task<ResponseApiDTO<IEnumerable<UserResponseDTO>>> GetUsersAsync(string? search, string? sortBy, int pageIndex = 1)
        {
            var allUsers = await _userRepo.GetUsersAsync();

            if(!string.IsNullOrEmpty(search))
            {
                allUsers = allUsers.Where(u => u.FullName.Contains(search));
            }

            allUsers = allUsers.OrderBy(u => u.FullName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "cc": allUsers = allUsers.OrderByDescending(u => u.FullName); break;
                }
            }

            var result = PaginatedList<DataAccessLayer.Entity.User>.Create(allUsers, pageIndex, PAGE_SIZE);

            if(result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<UserResponseDTO>>
                {
                    Success = true,
                    Message = "Does not have any result match your request."
                };
            }

            var userResponseDTOs = new List<UserResponseDTO>();

            foreach (var user in result)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserResponseDTO>(user);
                userDto.Roles = roles; // Assuming UserRequestDTO has a Roles property
                userResponseDTOs.Add(userDto);
            }

            return new ResponseApiDTO<IEnumerable<UserResponseDTO>>
            {
                Success = true,
                Message = "Here is current users match your request.",
                Data = userResponseDTOs
            };
        }
        public async Task<ResponseApiDTO> UpdateUserAsync(string id, UserUpdateDTO userUpdateDTO)
        {
            var updatedUser = await _userRepo.GetByIdAsync(id);

            if(updatedUser == null)
            {
                return new ResponseApiDTO
                {
                    Success = true,
                    Message = "User not found, please try again."
                };
            }

            var user = _mapper.Map(userUpdateDTO, updatedUser);
            var result = await _userRepo.UpdateUserAsync(user);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Update user failed, please try again. \nPlease check your information. \nPassword phai co chu hoa, chu thuong, va ki tu dac biet."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Update user succesfully."
            };
        }

        public async Task<ResponseApiDTO> UpdateUserRoleAsync(string id, string oldRole, string newRole, string choice)
        {
            var updatedUser = await _userRepo.GetByIdAsync(id);
            if (updatedUser == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "User not found, please try again."
                };
            }

            var updatedUserRoles = await _userManager.GetRolesAsync(updatedUser);

            switch (choice)
            {
                case "replace":
                    // Xử lý trường hợp thay thế một vai trò cụ thể
                    if (updatedUserRoles.Contains(oldRole))
                    {
                        // Xóa vai trò cũ
                        await DeleteUserRoleAsync(updatedUser.Id, oldRole);
                        await _context.SaveChangesAsync();

                        // Thêm vai trò mới
                       return await AddUserRoleAsync(updatedUser.Id, newRole);
                    }
                    else
                    {
                        return new ResponseApiDTO
                        {
                            Success = false,
                            Message = $"The user does not have the role {oldRole}."
                        };
                    }

                case "add":
                    // Thêm vai trò mới mà không cần xóa vai trò cũ
                    return await AddUserRoleAsync(updatedUser.Id, newRole);

                case "remove":
                    // Xóa một vai trò cụ thể
                    return await DeleteUserRoleAsync(updatedUser.Id, oldRole);

                default:
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = "Invalid choice. Please specify 'replace', 'add', or 'remove'."
                    };
            }
        }

        public async Task<ResponseApiDTO> AddUserRoleAsync(string id, string role)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if(user == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if(await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.AddToRoleAsync(user, role);
                await _context.SaveChangesAsync();

                if(role.Equals(RoleDTO.Tutor))
                {
                    var tutorDTO = new TutorDTO();
                    tutorDTO.UserId = user.Id;
                    await _tutorService.AddTutorAsync(tutorDTO);
                }

                if (!result.Succeeded)
                {
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = "Add role for user failed. \nPlease try again"
                    };
                }

                return new ResponseApiDTO
                {
                    Success = true,
                    Message = "Add role for user successfully."
                };
            }
            else
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "This role does not exists. \nPlease try again."
                };
            }
        }

        public async Task<ResponseApiDTO> DeleteUserRoleAsync(string id, string role)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if(user == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "User not found. \nPlease try again"
                };
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Contains(role))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                    await _context.SaveChangesAsync();

                    if(role.Equals(RoleDTO.Tutor))
                    {
                        var tutor = await _tutorService.GetTutorByUserIdAsync(user.Id);
                        await _tutorService.DeleteTutorAsync(tutor.Id);
                    } 

                    if(!result.Succeeded)
                    {
                        return new ResponseApiDTO
                        {
                            Success = false,
                            Message = "Delete role from user failed. \nPlease try again"
                        };
                    }

                    return new ResponseApiDTO
                    {
                        Success = true,
                        Message = "Delete role from user successfully."
                    };
                }
                else
                {
                    return new ResponseApiDTO
                    {
                        Success = false,
                        Message = "User does not have this role. \nPlease try again."
                    };
                }
            }
            else
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "This role does not exist. \nPlease try again"
                };
            }

        }

        public async Task<ResponseApiDTO<IEnumerable<CoinDTOWithId>>> GetTransactionsAsync(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var records = await _coinManagementRepo.GetAllAsync();

            if(!string.IsNullOrEmpty(search))
            {
                records = records.Where(x => x.UserId == search);
            }

            if(from.HasValue)
            {
                records = records.Where(x => x.Date >= from.Value);
            }

            if(to.HasValue)
            {
                records = records.Where(x => x.Date <= to.Value);
            }

            records = records.OrderBy(x => x.Date);

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": records = records.OrderByDescending(x => x.Date); break;
                }
            }

            var result = PaginatedList<CoinManagement>.Create(records, page, PAGE_SIZE);

            return new ResponseApiDTO<IEnumerable<CoinDTOWithId>>
            {
                Success = true,
                Message = "Đây là danh sách các giao dịch của người dùng",
                Data = result.Select(x => new CoinDTOWithId
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Coin = x.Coin,
                    Date = x.Date,
                })
            };
        }

        public async Task<ResponseApiDTO> LockUserAsync(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng."
                };
            }

            user.IsLocked = true;
            var update = await _userRepo.UpdateUserAsync(user);
            if (!update.Succeeded)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi khóa tài khoản."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Khóa tài khoản thành công."
            };
        }
    }
}
