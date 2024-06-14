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
    public class StudentJoinsController : ControllerBase
    {
        private readonly IStudentJoinService _studentJoinService;

        public StudentJoinsController(IStudentJoinService studentJoinService)
        {
            _studentJoinService = studentJoinService;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("create-student-join")]
        public async Task<IActionResult> CreateAsync(int subjectLevelId)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _studentJoinService.CreateAsync(new StudentJoinRequestDTO { UserId = userId, SubjectLevelId = subjectLevelId});

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Tutor, Student")]
        [HttpGet("student-join-list-for-subject-level")]
        public async Task<IActionResult> GetBySubjectLevelIdAsync(string subjectLevelId, string? userId, int page = 1)
        {
            var result = await _studentJoinService.GetAllBySubjectLevelIdAsync(subjectLevelId, userId, page);
            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("all-student-join-for-all-subject-level")]
        public async Task<IActionResult> GetAllSync(string? subjectLevelId, string? userId, int page = 1)
        {
            var result = await _studentJoinService.GetAllAsync(subjectLevelId, userId, page);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("all-student-join-for-student")]
        public async Task<IActionResult> GetAllForStudentSync(string? subjectLevelId, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _studentJoinService.GetAllByStudentIdAsync(userId, subjectLevelId, page);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Tutor")]
        [HttpDelete("delete-student-for-tutor")]
        public async Task<IActionResult> DeleteForTutorAsync(int studentJoinId)
        {
            var result = await _studentJoinService.DeleteForTutorAsync(studentJoinId);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("move-out")]
        public async Task<IActionResult> DeleteForStudentAsync(int studentJoinId)
        {
            var result = await _studentJoinService.DeleteForStudentAsync(studentJoinId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
