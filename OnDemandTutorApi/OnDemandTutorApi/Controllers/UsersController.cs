using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.Security.Claims;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Tutor, Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly MyDbContext _context;
        private readonly IRequestService _requestService;


        public UsersController(IUserService userService, IEmailService emailService, UserManager<User> userManager, IMapper mapper, MyDbContext context, IRequestService requestService)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _requestService = requestService;
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserRequestDTO userDto)
        {
            var result = await _userService.SignUpAsync(userDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserAuthenDTO userAuthen)
        {
            var result = await _userService.SignInAsync(userAuthen);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost("RenewToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RenewToken(TokenDTO tokenDTO)
        {
            var result = await _userService.RenewTokenAsync(tokenDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(UserForgotPassDTO forgotPassUser)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassUser.Email);

            if (user == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Success = false,
                    Message = "Could not send link to email, please try again. \nYour email does not exist in system."
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var forgotPasswordLink = Url.Action(nameof(ResetPasswordView), "Users", new { token, email = user.Email }, Request.Scheme);
            Console.WriteLine("Link: " + forgotPasswordLink);
            var message = new EmailDTO
                (
                    new string[] { user.Email! },
                    "Forgot Password Link!",
                    forgotPasswordLink!
                );

            _emailService.SendEmail(message);

            return Ok(new ResponseDTO
            {
                Success = true,
                Message = $"Password changed request is sent on your Email {user.Email}.Please open your email and click the link."
            });
        }

        [HttpGet("ResetPasswordView")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordView(string token, string email)
        {
            var resetPassVM = new UserResetPassDTO { Token = token, Email = email };
            return Ok(new { resetPassVM });
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassAsync(UserResetPassDTO userReset)
        {
            var result = await _userService.ResetPassAsync(userReset);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfileAysnc()
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("1:" + userId);
            Console.ResetColor();

            var user = await _userManager.FindByIdAsync(userId);

            Console.WriteLine("user id " + user.Id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO<UserGetProfileDTO>
                {
                    Success = false,
                    Message = "User not found.",
                });
            }

            var userProfile = _mapper.Map<UserGetProfileDTO>(user);

            return StatusCode(StatusCodes.Status200OK, new ResponseDTO<UserGetProfileDTO>
            {
                Success = true,
                Message = "Get user profile successfully.",
                Data = userProfile
            });
        }

        [Authorize]
        [HttpPut("UpdatUserProfile")]
        public async Task<IActionResult> UpdatUserProfile(UserProfileUpdateDTO userUpdate)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO<UserGetProfileDTO>
                {
                    Success = false,
                    Message = "User not found.",
                });
            }

            var userProfileUpdate = _mapper.Map(userUpdate, user);

            var result = await _userManager.UpdateAsync(userProfileUpdate);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO<UserGetProfileDTO>
                {
                    Success = false,
                    Message = "Error occur when update user profile, please try again."
                });
            }

            var userProfile = _mapper.Map<UserGetProfileDTO>(user);

            return StatusCode(StatusCodes.Status200OK, new ResponseDTO<UserGetProfileDTO>
            {
                Success = true,
                Message = "Update user profile successfully.",
                Data = userProfile
            });

        }
        [HttpGet("GetAllRequest")]
        public async Task<IActionResult> GetAllRequestAsync(string? search, string? sortBy, int pageIndex = 1)
        {
            var result = await _requestService.GetAllRequestAsync(search, sortBy, pageIndex);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("GetUserByRequestID")]
        public async Task<IActionResult> GetRequestByRequestIDAsync(int id, string? search, string? sortBy, int pageIndex = 1)
        {
            var result = await _requestService.GetRequestByRequestIDAsync(id, search, sortBy, pageIndex);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("GetUserByUserID")]
        public async Task<IActionResult> GetRequestByUserIDAsync(string id, string? search, string? sortBy, int pageIndex = 1)
        {
            var result = await _requestService.GetRequestByUserIDAsync(id, search, sortBy, pageIndex);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /* [HttpPost("CreateRequest")]
         public async Task<IActionResult> CreateRequestAsync(RequestDTO Request)
         {
             var result = await _requestService.CreateRequestAsync(Request);

             if (!result.Success)
             {
                 return StatusCode(StatusCodes.Status400BadRequest, result);
             }

             return StatusCode(StatusCodes.Status200OK, result);
         }
        */

        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequestAsync(RequestDTO Request)
        {
            /*
             -- handle bussiness logic
            example 
            bussiness logic:
            status should be in range: ["", "" ,""] -> disable, enable, on-going,....
            create enum -> define type: -> disable, enable, on-going,....
             */
            if (Constants.LIST_STATUS.Contains(Request.Status))
            {
                var result = await _requestService.CreateRequestAsync(Request);

                if (!result.Success)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid status state");
            }

        }

        [HttpPut("UpdateRequest")]
        public async Task<IActionResult> UpdateRequestAsync(int id, RequestDTO Request)
        {
            var result = await _requestService.UpdateRequestAsync(id, Request);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequestAsync(int id)
        {
            var result = await _requestService.DeleteRequestAsync(id);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
