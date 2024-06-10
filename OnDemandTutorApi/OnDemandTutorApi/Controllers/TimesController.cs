using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

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
    }
}
