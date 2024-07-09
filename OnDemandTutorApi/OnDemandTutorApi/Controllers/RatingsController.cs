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
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("create-rating")]
        public async Task<IActionResult> CreateAsync(RatingRequestDTO ratingRequestDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _ratingService.CreateAsync(userId, ratingRequestDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("get-ratings-by-tutor-id")]
        public async Task<IActionResult> GetAllByTutorIdAsynnc(int tutorId, string? sortBy, int page = 1)
        {
            var result = await _ratingService.GetAllByTutorIdAsync(tutorId, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Tutor")]
        [HttpGet("get-ratings-by-tutor-self")]
        public async Task<IActionResult> GetAllByTutorSelfAsync(string? sortBy, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _ratingService.GetAllByTutorSelfAsync(userId, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("get-ratings-by-student-id")]
        public async Task<IActionResult> GetAllByStudentIdAsync(string? sortBy, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _ratingService.GetAllByStudentIdAsync(userId, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("get-all-ratings")]
        public async Task<IActionResult> GetAllAsync(string? userId, int tutorId, string? sortBy, int page = 1)
        {
            var result = await _ratingService.GetAllAsync(userId, tutorId, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Student")]
        [HttpPut("update-rating")]
        public async Task<IActionResult> UpdateAsync(int ratingId, RatingUpdateDTO ratingUpdateDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _ratingService.UpdateAsync(ratingId, userId, ratingUpdateDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Student")]
        [HttpDelete("delete-rating-for-student")]
        public async Task<IActionResult> DeleteForStudentAsync(int ratingId)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _ratingService.DeleteForStudentAsync(ratingId, userId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize(Roles = "Moderator, Admin")]
        [HttpDelete("delete-rating")]
        public async Task<IActionResult> DeleteAsync(int ratingId)
        {
            var result = await _ratingService.DeleteAsync(ratingId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
