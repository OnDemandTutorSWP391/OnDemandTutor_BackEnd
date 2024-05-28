﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using OnDemandTutorApi.DataAccessLayer.Entity;
using System.Text.RegularExpressions;

namespace OnDemandTutorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService, IEmailService emailService, UserManager<User> userManager)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
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
            if(!result.Success)
            {
                return Unauthorized();
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
                return BadRequest(new ResponseDTO
                {
                    Success = false,
                    Message = "Could not send link to email, please try again"
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var forgotPasswordLink = Url.Action(nameof(ResetPasswordView), "Users", new {token, email = user.Email}, Request.Scheme);
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
                Message = $"Password changed request is sent on your Email {user.Email}.Please open your email and click the link"
            });
        }

        [HttpGet("ResetPasswordView")]
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
    }
}
