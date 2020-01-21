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
    [Authorize]
    [ApiController]
    public class VideoHistoryController : ControllerBase
    {
        private readonly IVideoHistoryService _videoHistoryService;

        public VideoHistoryController(IVideoHistoryService videoHistoryService)
        {
            _videoHistoryService = videoHistoryService;
        }
        
        [HttpPost("set")]
        public async Task SetVideoProgress(Guid videoId, long time = 0)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            await _videoHistoryService.SetProgressAsync(userId, videoId, time);
        }
        
        [HttpGet("history")]
        public async Task<VideoHistoryDto> GetUserVideoHistory()
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _videoHistoryService.GetHistoryAsync(userId);
        }

        [HttpGet("progressVideo/{videoId}")]
        public async Task<VideoRecordDto> GetVideoProgress(Guid videoId)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _videoHistoryService.GetVideoProgressAsync(userId, videoId);
        }

        
        [HttpGet("progress/{courseId}")]
        public async Task<int> GetUserProgres(Guid courseId)
        {
            Guid userId = Guid.Empty;
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = Guid.Parse(identity.Claims.First().Value);
            }
            return await _videoHistoryService.GetProgressAsync(userId, courseId);
        }
    }
}