﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;

namespace OnDemandTutorApi.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpPost("send-response")]
        public async Task<IActionResult> SendResponseAsync(int requestId, ResponseContentDTO responseContent)
        {
            var result = await _responseService.SendResponseAsync(requestId, responseContent);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(string? search, string? requestId, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var result = await _responseService.GetAllAsync(search, requestId, from, to, sortBy, page);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
