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
        private readonly IQueryable<Quiz> _quizzes;
        public QuizRepository(PixBlocksContext context)
        {
            _entities = context;
            _quizzes = context.Quizzes.Include(x => x.Questions).ThenInclude(x => x.Answers);
        }

        public async Task<Quiz> GetAsync(Guid id) => await _quizzes.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Quiz> GetAsync(Media media) => await _quizzes.SingleOrDefaultAsync(x => x.MediaId == media.Id);

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
            var questions = await _entities.QuizQuestions.Where(x => x.QuizId == quiz.Id).ToListAsync();
            foreach(var question in questions)
            {
                var answers = await _entities.QuizAnswers.Where(x => x.QuizQuestionId == question.Id).ToListAsync();
                _entities.QuizAnswers.RemoveRange(answers);
            }
            _entities.QuizQuestions.RemoveRange(questions);

            _entities.Quizzes.Update(quiz);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Quiz quiz)
        {
            var questions = await _entities.QuizQuestions.Where(x => x.QuizId == quiz.Id).ToListAsync();
            foreach (var question in questions)
            {
                var answers = await _entities.QuizAnswers.Where(x => x.QuizQuestionId == question.Id).ToListAsync();
                _entities.QuizAnswers.RemoveRange(answers);
            }
            _entities.QuizQuestions.RemoveRange(questions);
            _entities.Quizzes.Remove(quiz);
            await _entities.SaveChangesAsync();
        }
        
    }
}
