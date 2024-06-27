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
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutorsController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [Authorize(Roles = "Tutor")]
        [HttpPost("register-tutor")]
        public async Task<IActionResult> UpdateProfileAsync(ProfileRequestDTO profileTutorDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _tutorService.UpdateProfileAsync(userId, profileTutorDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("get-tutor-profile-by-id")]
        public async Task<IActionResult> GetProfileByIdAsync(int id)
        {
            var result = await _tutorService.GetProfileByIdAsync(id);

            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Tutor")]
        [HttpGet("get-tutor-profile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _tutorService.GetProfileAsync(userId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Student")]
        [HttpGet("get-all-tutors-for-student")]
        public async Task<IActionResult> GetAllTutorsForStudentAsync(string? search, string? sortBy, int page = 1)
        {
            var result = await _tutorService.GetAllTutorsForStudentAsync(search, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("get-all-tutors")]
        public async Task<IActionResult> GetAllTutorsAsync(string? search, string? sortBy, int page = 1)
        {
            var result = await _tutorService.GetAllTutorsAsync(search, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut("update-status-profile")]
        public async Task<IActionResult> UpdateStatusAsync(int tutorId, string status)
        {
            var result = await _tutorService.UpdateStatusAsync(tutorId, status);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}
