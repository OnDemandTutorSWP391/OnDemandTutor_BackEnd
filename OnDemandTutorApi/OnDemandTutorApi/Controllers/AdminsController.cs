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

        [HttpPut("Lock-user-account")]
        public async Task<IActionResult> LockUserAsync(string id)
        {
            var result = await _adminService.LockUserAsync(id);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("get-all-transaction")]
        public async Task<IActionResult> GetTransactionsAsync(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var result = await _adminService.GetTransactionsAsync(search, from, to, sortBy, page);

            if(!result.Success)
            {
                return BadRequest("Hệ thống xảy ra lỗi khi cố truy cập vào danh sách các giao dịch của người dùng.");
            }

            return Ok(result);
        }
    }
}
