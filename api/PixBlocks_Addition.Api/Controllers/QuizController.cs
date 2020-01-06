using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task CreateQuiz([FromBody]CreateQuizResource quiz)
        {
            await _quizService.CreateQuizAsync(quiz);
        }

        [HttpGet("{id}")]
        public async Task<QuizDto> GetQuiz(Guid id) 
            => await _quizService.GetAsync(id);

        [Authorize(Roles = "Administrator")]
        [HttpPut("update")]
        public async Task UpdateQuiz([FromBody]UpdateQuizResource quiz) 
            => await _quizService.UpdateQuizAsync(quiz);

        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete")]
        public async Task RemoveQuiz(Guid id)
            => await _quizService.RemoveQuizAsync(id);

    }
}