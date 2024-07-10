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

        //CREATE

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

        //=================//

        //READ

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

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet("get-all-subject-level-for-staff")]
        public async Task<IActionResult> GetAllForStaffAsync(string? level, string? subject, string? tutor, int page = 1)
        {
            var result = await _subjectLevelService.GetAllForStaffAsync(level, subject, tutor, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-subject-level-by-id")]
        public async Task<IActionResult> GetByIdAsync(int subjectLevelId)
        {
            var result = await _subjectLevelService.GetByIdAsync(subjectLevelId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-all-subject-level-by-tutorId")]
        public async Task<IActionResult> GetAllByTutorIdAsync(string? level, string? subject, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _subjectLevelService.GetAllByTutorIdAsync(userId, level, subject, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //===========================//

        //UPDATE

        [Authorize(Roles = "Tutor")]
        [HttpPut("update-subject-level-for-tutor")]
        public async Task<IActionResult> UpdateAsync(int id, SubjectLevelRequestDTO subjectLevelDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _subjectLevelService.UpdateForTutorAsync(id, userId, subjectLevelDTO);
            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        //===========================//

        //DELETE

        [Authorize(Roles = "Tutor")]
        [HttpDelete("delete-subject-level-for-tutor")]
        public async Task<IActionResult> DeleteForTutorAsync(int id)
        {
            var result = await _subjectLevelService.DeleteForTutorAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("delete-subject-level-for-staff")]
        public async Task<IActionResult> DeleteForStaffAsync(int id)
        {
            var result = await _subjectLevelService.DeleteForStaffAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
