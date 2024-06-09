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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

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
    }
}
