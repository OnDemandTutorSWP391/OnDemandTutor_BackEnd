using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserRequestDTO userDto)
        {
            var result = await _userService.SignUpAsync(userDto);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserAuthenDTO userAuthen)
        {
            var result = await _userService.SignInAsync(userAuthen);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("ViewProfile")]
        [Authorize] 
        public async Task<IActionResult> ViewProfile(string userId)
        {
            var result = await _userService.GetUserProfileAsync(userId);
            if(result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        } 
    }
}
