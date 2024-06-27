using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using System.Security.Claims;

namespace OnDemandTutorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimesController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [Authorize(Roles = "Tutor")]
        [HttpPost("create-time")]
        public async Task<IActionResult> CreateAsync(TimeRequestDTO timeRequestDTO)
        {
            var result = await _timeService.CreateAsync(timeRequestDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("times-for-student")]
        public async Task<IActionResult> GetAllForStudentAsync(string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _timeService.GetAllForStudentAsync(userId, timeId, subjectLevelId, sortBy, from, to, page);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Tutor")]
        [HttpGet("times-for-Tutor")]
        public async Task<IActionResult> GetAllFortutorAsync(string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _timeService.GetAllForTutorAsync(userId, timeId, subjectLevelId, sortBy, from, to, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("times-for-mod")]
        public async Task<IActionResult> GetAllAsync(string? timeId, string? subjectLevelId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {

            var result = await _timeService.GetAllAsync(timeId, subjectLevelId, sortBy, from, to, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Tutor")]
        [HttpPut("update-time-for-tutor")]
        public async Task<IActionResult> UpdateAsync(int timeId, TimeRequestDTO timeRequest)
        {
            var result = await _timeService.UpdateAsync(timeId, timeRequest);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Tutor")]
        [HttpDelete("delete-time-for-tutor")]
        public async Task<IActionResult> DeleteForTutorAsync(int timeId)
        {
            var result = await _timeService.DeleteForTutorAsync(timeId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("delete-time-for-staff")]
        public async Task<IActionResult> DeleteForStaffAsync(int timeId)
        {
            var result = await _timeService.DeleteForStaffAsync(timeId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
