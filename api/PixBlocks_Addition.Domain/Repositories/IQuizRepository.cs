using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task AddAsync(Quiz quiz);
        Task<Quiz> GetAsync(Guid id);
        Task<Quiz> GetAsync(Media media);
        Task<Media> GetMediaAsync(Guid quizId);
        Task UpdateAsync(Quiz quiz);
        Task RemoveAsync(Quiz quiz);
    }
}
