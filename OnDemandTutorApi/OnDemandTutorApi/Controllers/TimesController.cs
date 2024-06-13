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
        public async Task<IActionResult> GetAllForStudentAsync(string? timeId, string? sortBy, DateTime? from, DateTime? to, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _timeService.GetAllForStudentAsync(userId, timeId, sortBy, from, to, page);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
