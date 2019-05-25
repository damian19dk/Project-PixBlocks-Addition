using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;
using PixBlocks_Addition.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task CreateVideo([FromBody]MediaResource video)
        {
            await _videoService.AddAsync(video);
        }

        [HttpGet]
        public async Task<VideoDto> GetVideo(string mediaId)
        {
            return await _videoService.GetAsync(mediaId);
        }

        [HttpGet("browse")]
        public async Task<IEnumerable<VideoDto>> Browse(string title)
        {
            return await _videoService.BrowseAsync(title);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<VideoDto>> GetAll()
        {
            return await _videoService.GetAllAsync();
        }

        [HttpGet("all_paging")]
        public async Task<IEnumerable<VideoDto>> GetAll(int page, int count = 10)
        {
            return await _videoService.GetAllAsync(page, count);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task Remove(Guid id)
        {
            await _videoService.RemoveAsync(id);
        }
    }
}