using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserCourseHistoryService _userCourseHistoryService;

        public HistoryController(IUserService userService, IUserCourseHistoryService userCourseHistoryService)
        {
            _userService = userService;
            _userCourseHistoryService = userCourseHistoryService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("deleteUserHistory")]
        public async Task RemoveUserhistory()
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _userCourseHistoryService.RemoveAsync(userId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("addCourseToHistory")]
        public async Task AddCourseToHistory(Guid courseId)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _userCourseHistoryService.AddHistoryAsync(userId, courseId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getUserHistory")]
        public async Task<IEnumerable<CourseDto>> GetUserHistory()
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _userCourseHistoryService.GetAllAsync(userId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("clearUserHistory")]
        public async Task ClearUserHistory()
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _userCourseHistoryService.CleanUserHistory(userId);
        }
    }
}
