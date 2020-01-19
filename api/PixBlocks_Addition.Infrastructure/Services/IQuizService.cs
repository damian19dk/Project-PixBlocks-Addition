using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes;
using System;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IQuizService
    {
        Task CreateQuizAsync(CreateQuizResource quiz);
        Task<QuizDto> GetAsync(Guid id);
        Task UpdateQuizAsync(UpdateQuizResource quiz);
        Task RemoveQuizAsync(Guid id);
    }
}
