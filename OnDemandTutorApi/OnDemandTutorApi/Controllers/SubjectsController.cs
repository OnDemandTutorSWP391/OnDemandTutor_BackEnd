using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost("create-subject")]
        public async Task<IActionResult> CreateAsync(SubjectDTO subjectDTO)
        {
            var result = await _subjectService.CreateAsync(subjectDTO);
            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(string? search, string? subjectId, string? sortBy, int page = 1)
        {
            var result = await _subjectService.GetAllAsync(search, subjectId, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut("update-subject")]
        public async Task<IActionResult> UpdateAsync(int id, SubjectDTO subjectDTO)
        {
            var result = await _subjectService.UpdateAsync(id, subjectDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpDelete("delete-subject")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _subjectService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
