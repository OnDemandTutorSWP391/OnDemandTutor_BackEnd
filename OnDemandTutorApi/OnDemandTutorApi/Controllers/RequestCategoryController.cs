using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    //[Authorize(Roles = "Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestCategoryController : ControllerBase
    {
        private readonly IRequestCategoryService _requestCategoryService;

        public RequestCategoryController(IRequestCategoryService requestCategoryService)
        {
            _requestCategoryService = requestCategoryService;
        }

        [HttpPost("create-request-category")]
        public async Task<IActionResult> CreateAsync(RequestCategoryDTO requestCategoryDTO)
        {
            var result = await _requestCategoryService.CreateAsync(requestCategoryDTO);

            if (!result.Success) 
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllAsync(string? search, string? sortBy, int page = 1)
        {
            var result = await _requestCategoryService.GetAllAsync(search, sortBy, page);

            if(!result.Success)
            {
                return BadRequest("Hệ thống gặp lỗi khi truy cập danh sách các loại yêu cầu.");
            }

            return Ok(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _requestCategoryService.GetByIdAsync(id);

            if(!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateAsync(RequestCategoryDTO requestCategoryDTO, int id)
        {
            var result = await _requestCategoryService.UpdateAsync(requestCategoryDTO, id);
            
            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _requestCategoryService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
