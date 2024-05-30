using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using System.Reflection.Metadata.Ecma335;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
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
    }
}
