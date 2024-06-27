using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Moderator, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelsController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpPost("create-level")]
        public async Task<IActionResult> CreateAsync(LevelDTO levelDTO)
        {
            var result = await _levelService.CreateAsync(levelDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(string? search, string? levelId, string? sortBy, int page = 1)
        {
            var result = await _levelService.GetAllAsync(search, levelId, sortBy, page);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("update-level")]
        public async Task<IActionResult> UpdateAsync(int id, LevelDTO levelDTO)
        {
            var result = await _levelService.UpdateAsync(id, levelDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("delete-level")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _levelService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
