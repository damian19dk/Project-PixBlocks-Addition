﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Create([FromForm]MediaResource lesson)
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
        [HttpPut("change")]
        public async Task Put([FromForm]ChangeMediaResource resource)
            => await _lessonService.UpdateAsync(resource);

        [Authorize(Roles = "Administrator")]
        [HttpDelete("video")]
        public async Task RemoveVideo(Guid lessonId, Guid videoId)
        {
            await _lessonService.RemoveVideoFromLessonAsync(lessonId, videoId);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task Remove(Guid Id)
        {
            await _lessonService.RemoveAsync(Id);
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

        [HttpGet("tags")]
        public async Task<IEnumerable<LessonDto>> GetAll(params string[] tags)
            => await _lessonService.GetAllByTagsAsync(tags);

        [HttpGet("all")]
        public async Task<IEnumerable<LessonDto>> GetAll(int page, int count = 10)
        {
            return await _lessonService.GetAllAsync(page, count);
        }

        [HttpGet("count")]
        public async Task<int> Count()
        {
            return await _lessonService.CountAsync();
        }

    }
}