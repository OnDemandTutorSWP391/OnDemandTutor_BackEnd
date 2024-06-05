using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public AdminsController(IAdminService adminService, IEmailService emailService,IUserService userService, UserManager<User> userManager)
        {
            _adminService = adminService;
            _emailService = emailService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetUsersAysnc(string? search, string? sortBy, int pageIndex = 1)
        {
            var result = await _adminService.GetUsersAsync(search, sortBy, pageIndex);

            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(UserRequestDTO userRequest)
        {
            var result = await _adminService.CreateUserAsync(userRequest);

            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(string id, UserUpdateDTO userUpdateDTO)
        {
            var result = await _adminService.UpdateUserAsync(id, userUpdateDTO);

            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRoleAsync(string id, string oldRole, string newRole, string choice)
        {
            var result = await _adminService.UpdateUserRoleAsync(id, oldRole, newRole, choice);
            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var result = await _adminService.DeleteUserAsync(id);

            if(!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPost("SendResponse")]
        public async Task<IActionResult> SendResponseAsync(UserForgotPassDTO forgotPassUser)
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

            try
            {
                var emailDTO = new EmailDTO(
                    new List<string> { forgotPassUser.Email },
                    "Password Reset Request",
                    $"Hi {forgotPassUser.Email},<br><br>Your request has been responded to."
                );

                _emailService.SendEmail(emailDTO);

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = $"Your Request has been repplied."
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions when sending the email
                return StatusCode(500, new ResponseDTO
                {
                    Success = false,
                    Message = "Failed to send email. Please try again later."
                });
            }
        }
    }
}
