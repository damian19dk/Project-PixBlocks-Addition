using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class QuizRepository: IQuizRepository
    {
        private readonly PixBlocksContext _entities;

        public QuizRepository(PixBlocksContext context)
        {
            _entities = context;
        }

        public async Task<Quiz> GetAsync(Guid id) => await _entities.Quizzes.Include(q=>q.Answers).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Quiz> GetAsync(Media media) => await _entities.Quizzes.Include(q => q.Answers).SingleOrDefaultAsync(x => x.MediaId == media.Id);

        public async Task<Media> GetMediaAsync(Guid quizId)
        {
            Media media = await _entities.Courses.SingleOrDefaultAsync(x => x.QuizId == quizId);
            if(media == null)
            {
                media = await _entities.Videos.SingleOrDefaultAsync(x => x.QuizId == quizId);
            }
            return media;
        }

        public async Task AddAsync(Quiz quiz)
        {
            _entities.Quizzes.Add(quiz);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            var answers = await _entities.QuizAnswers.Where(x => x.QuizId == quiz.Id).ToListAsync();
            _entities.QuizAnswers.RemoveRange(answers);
            _entities.Quizzes.Update(quiz);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Quiz quiz)
        {
            var answers = await _entities.QuizAnswers.Where(x => x.QuizId == quiz.Id).ToListAsync();
            _entities.QuizAnswers.RemoveRange(answers);
            _entities.Quizzes.Remove(quiz);
            await _entities.SaveChangesAsync();
        }
        
    }
}
