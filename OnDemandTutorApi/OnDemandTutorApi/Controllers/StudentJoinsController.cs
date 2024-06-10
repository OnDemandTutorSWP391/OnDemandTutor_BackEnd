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

        [HttpPost("create-student-join")]
        public async Task<IActionResult> CreateAsync(int subjectLevelId)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _studentJoinService.CreateAsync(new StudentJoinDTO { UserId = userId, SubjectLevelId = subjectLevelId});

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
