using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task Create(MediaResource lesson)
        {
            await _lessonService.CreateAsync(lesson);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("video")]
        public async Task AddVideo([FromBody]UploadResource upload)
        {
            await _lessonService.AddVideoAsync(upload);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("video")]
        public async Task RemoveVideo(Guid lessonId, Guid videoId)
        {
            await _lessonService.RemoveVideoFromLessonAsync(lessonId, videoId);
        }

        [HttpGet]
        public async Task<LessonDto> Get(Guid id)
        {
            return await _lessonService.GetAsync(id);
        }

        [HttpGet("title")]
        public async Task<IEnumerable<LessonDto>> Get(string title)
        {
            return await _lessonService.GetAsync(title);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<LessonDto>> GetAll()
        {
            return await _lessonService.GetAllAsync();
        }
            
        [HttpGet("all paging")]
        public async Task<IEnumerable<LessonDto>> GetAll(int page, int count = 10)
        {
            return await _lessonService.GetAllAsync(page, count);
        }

    }
}