using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.Security.Claims;
using System.Text.RegularExpressions;

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

        public UsersController(IUserService userService, IEmailService emailService, UserManager<User> userManager, IMapper mapper, MyDbContext context)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context; 
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

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string token, string email)
        {
            var result = await _userService.ConfirmEmailAsync(token, email);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserAuthenDTO userAuthen)
        {
            var result = await _userService.SignInAsync(userAuthen);
            if(!result.Success)
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
            if(!result.Success)
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
                return BadRequest(new ResponseApiDTO
                {
                    Success = false,
                    Message = "Could not send link to email, please try again. \nYour email does not exist in system."
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // var forgotPasswordLink = $"https://localhost:5173/api/Users/reset-password-view?token={token}&email={user.Email}";
            var forgotPasswordLink = $"https://localhost:7259/api/Users/reset-password-view?token={token}&email={user.Email}";
            var message = new EmailDTO
                (
                    new string[] { user.Email! },
                    "Forgot Password Link!",
                    forgotPasswordLink!
                );

            _emailService.SendEmail(message);

            return Ok(new ResponseApiDTO
            {
                Success = true,
                Message = $"Password changed request is sent on your Email {user.Email}.Please open your email and click the link."
            });
        }

        [HttpGet("reset-password-view")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordView(string token, string email)
        {
            var resetPassVM = new UserResetPassDTO { Token = token, Email = email };
            return Ok(new {resetPassVM});
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassAsync(UserResetPassDTO userReset)
        {
            var result = await _userService.ResetPassAsync(userReset);
            if(!result.Success) 
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

            var result = await _userService.GetUserProfileAysnc(userId);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status404NotFound, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [Authorize]
        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdatUserProfileAsync(UserProfileUpdateDTO userUpdate)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _userService.UpdatUserProfileAsync(userId, userUpdate);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [AllowAnonymous]
        [HttpPut("update-status-user")]
        public async Task<IActionResult> UpdateUserStatusAsync(bool status)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _userService.UpdateUserStatusAsync(userId, status);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [Authorize]
        [HttpPut("lock-account")]
        public async Task<IActionResult> DeleteUserAsync()
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _userService.DeleteUserAsync(userId);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
