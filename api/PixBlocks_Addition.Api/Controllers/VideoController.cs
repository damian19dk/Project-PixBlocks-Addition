using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;
using PixBlocks_Addition.Infrastructure.DTOs;

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

        [HttpGet("all")]
        public async Task<IEnumerable<VideoDto>> GetAll()
        {
            return await _videoService.GetAllAsync();
        }

        [HttpDelete]
        public async Task Remove(Guid id)
        {
            await _videoService.RemoveAsync(id);
        }
    }
}