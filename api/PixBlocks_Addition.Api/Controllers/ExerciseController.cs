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
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task Create(MediaResource exercise)
        {
            await _exerciseService.CreateAsync(exercise);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("video")]
        public async Task AddVideo([FromBody]UploadResource upload)
        {
            await _exerciseService.AddVideoAsync(upload);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("video")]
        public async Task RemoveVideo(Guid exerciseId, Guid videoId)
        {
            await _exerciseService.RemoveVideoFromExerciseAsync(exerciseId, videoId);
        }

        [HttpGet]
        public async Task<ExerciseDto> Get(Guid id)
        {
            return await _exerciseService.GetAsync(id);
        }

        [HttpGet("title")]
        public async Task<IEnumerable<ExerciseDto>> Get(string title)
        {
            return await _exerciseService.GetAsync(title);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ExerciseDto>> GetAll()
        {
            return await _exerciseService.GetAllAsync();
        }

        [HttpGet("allPaging")]
        public async Task<IEnumerable<ExerciseDto>> GetAll(int page, int count = 10)
        {
            return await _exerciseService.GetAllAsync(page, count);
        }
    }
}