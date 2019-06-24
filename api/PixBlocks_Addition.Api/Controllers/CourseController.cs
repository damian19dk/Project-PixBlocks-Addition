using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task Create([FromForm]MediaResource course)
        {
            await _courseService.CreateAsync(course);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("video")]
        public async Task AddVideo([FromBody]UploadResource upload)
        {
            await _courseService.AddVideoAsync(upload);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("video")]
        public async Task RemoveVideo(Guid courseId, Guid videoId)
        {
            await _courseService.RemoveVideoFromCourseAsync(courseId, videoId);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task Remove(Guid courseId)
        {
            await _courseService.RemoveAsync(courseId);
        }

        [HttpGet("title")]
        public async Task<IEnumerable<CourseDto>> Get(string title)
        {
            return await _courseService.GetAsync(title);
        }

        [HttpGet]
        public async Task<CourseDto> Get(Guid id)
        {
            return await _courseService.GetAsync(id);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("change")]
        public async Task Put([FromForm]ChangeMediaResource resource)
            => await _courseService.UpdateAsync(resource);

        [HttpGet("tags")]
        public async Task<IEnumerable<CourseDto>> GetAll(params string[] tags)
            => await _courseService.GetAllByTagsAsync(tags);

        [HttpGet("all")]
        public async Task<IEnumerable<CourseDto>> GetAll(int page, int count = 10)
        {
            return await _courseService.GetAllAsync(page, count);
        }

    }
}