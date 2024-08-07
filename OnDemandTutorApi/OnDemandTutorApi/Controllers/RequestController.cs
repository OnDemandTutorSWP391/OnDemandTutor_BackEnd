﻿using Microsoft.AspNetCore.Authorization;
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
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [Authorize(Roles = "Tutor, Student")]
        [HttpPost("create-request")]
        public async Task<IActionResult> CreateAsync(RequestDTO requestDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var result = await _requestService.CreateAsync(userId, requestDTO);

            if(!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Tutor, Student")]
        [HttpGet("get-all-for-user")]
        public async Task<IActionResult> GetAllForUserAsync(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            var result = await _requestService.UserGetAllAsync(userId, search, from, to, sortBy, page);

            if(!result.Success)
            {
                return BadRequest("Hệ thống gặp lỗi khi truy cập danh sách các yêu cầu của bạn.");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(string? search, string? userId, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var result = await _requestService.GetAllAsync(search, userId, from, to, sortBy, page);

            if (!result.Success)
            {
                return BadRequest("Hệ thống gặp lỗi khi truy cập danh sách các yêu cầu.");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut("update-status-by-id")]
        public async Task<IActionResult> UpdateStatusRequestAsync(int id, RequestUpdateStatusDTO request)
        {
            var result = await _requestService.UpdateStatusRequestAsync(id, request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student, Tutor")]
        [HttpPut("update-by-id")]
        public async Task<IActionResult> UpdateAsync(int id, RequestDTO request)
        {
            var result = await _requestService.UpdateAsync(id, request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Moderator, Admin, Tutor, Student")]
        [HttpDelete("delete-request-by-id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _requestService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
