using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoHistoryController : ControllerBase
    {
        private readonly IVideoHistoryService _videoHistoryService;

        public VideoHistoryController(IVideoHistoryService videoHistoryService)
        {
            _videoHistoryService = videoHistoryService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("addVideoToHistory")]
        public async Task AddVideoToHistory(Guid videoId, long time = 0)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _videoHistoryService.AddAsync(userId, videoId, time);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("UpadateVideoHistory")]
        public async Task UpdateVideoHistory(Guid videoId, long time = 0)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _videoHistoryService.UpdateAsync(userId, videoId, time);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getUserVideoHistory")]
        public async Task<VideoHistoryDto> GetUserVideoHistory()
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _videoHistoryService.GetUserVideoHistory(userId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserProgres")]
        public async Task<int> GetUserProgres(Guid courseId)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _videoHistoryService.GetUserProgres(userId, courseId);
        }
    }
}