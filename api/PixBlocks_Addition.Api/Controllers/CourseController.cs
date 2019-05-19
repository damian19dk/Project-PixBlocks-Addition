﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("create")]
        public async Task Create(MediaResource course)
        {
            await _courseService.CreateAsync(course);
        }

        [HttpPost("video")]
        public async Task AddVideo([FromBody]UploadResource upload)
        {
            await _courseService.AddVideoAsync(upload);
        }

        [HttpDelete("video")]
        public async Task RemoveVideo(Guid courseId, Guid videoId)
        {
            await _courseService.RemoveVideoFromCourseAsync(courseId, videoId);
        }

        [HttpGet("title")]
        public async Task<CourseDto> Get(string title)
        {
            return await _courseService.GetAsync(title);
        }

        [HttpGet]
        public async Task<CourseDto> Get(Guid id)
        {
            return await _courseService.GetAsync(id);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            return await _courseService.GetAllAsync();
        }

    }
}