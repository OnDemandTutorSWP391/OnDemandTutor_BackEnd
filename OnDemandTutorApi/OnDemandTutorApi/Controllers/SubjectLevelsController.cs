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
    public class SubjectLevelsController : ControllerBase
    {
        private readonly ISubjectLevelService _subjectLevelService;

        public SubjectLevelsController(ISubjectLevelService subjectLevelService)
        {
            _subjectLevelService = subjectLevelService;
        }

        [Authorize(Roles = "Tutor")]
        [HttpPost("register-subject-level")]
        public async Task<IActionResult> CreateAsync(SubjectLevelRequestDTO subjectLevelDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _subjectLevelService.CreateAsync(userId, subjectLevelDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Tutor, Student")]
        [HttpGet("get-all-subject-level")]
        public async Task<IActionResult> GetAllAsync(string? level, string? subject, string? tutor, int page = 1)
        {
            var result = await _subjectLevelService.GetAllAsync(level, subject, tutor, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Tutor")]
        [HttpPut("update-subject-level")]
        public async Task<IActionResult> UpdateAsync(int id, SubjectLevelRequestDTO subjectLevelDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _subjectLevelService.UpdateAsync(id, userId, subjectLevelDTO);
            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
