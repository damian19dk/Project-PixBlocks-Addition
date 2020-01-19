using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
using PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, ICourseRepository courseRepository, IVideoRepository videoRepository, 
                           IAutoMapperConfig mapperConfig)
        {
            _quizRepository = quizRepository;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
            _mapper = mapperConfig.Mapper;
        }

        public async Task CreateQuizAsync(CreateQuizResource quiz)
        {
            Media media = await _courseRepository.GetAsync(quiz.MediaId);
            if(media == null)
            {
                media = await _videoRepository.GetAsync(quiz.MediaId);
            }
            if(media == null)
            {
                throw new MyException(MyCodesNumbers.MediaNotFound, $"Media with id {quiz.MediaId} not found.");
            }
            if(media.QuizId != null)
            {
                throw new MyException(MyCodesNumbers.QuizExists, $"The given media already has a quiz. Remove the quiz or choose another media.");
            }

            var newQuiz = new Quiz(media.Id);
            foreach (var question in quiz.Questions)
            {
                var newQuestion = new QuizQuestion(question.Question);
                foreach (var answer in question.Answers)
                {
                    newQuestion.Answers.Add(new QuizAnswer(answer.Answer, answer.IsCorrect));
                }
                newQuiz.Questions.Add(newQuestion);
            }
            await _quizRepository.AddAsync(newQuiz);

            media.SetQuiz(newQuiz.Id);
            if(media is Course)
            {
                await _courseRepository.UpdateAsync(media as Course);
            }
            else
            {
                await _videoRepository.UpdateAsync(media as Video);
            }
        }

        public async Task<QuizDto> GetAsync(Guid id) 
            => _mapper.Map<QuizDto>(await _quizRepository.GetAsync(id));

        public async Task RemoveQuizAsync(Guid id)
        {
            var quiz = await _quizRepository.GetAsync(id);
            if(quiz == null)
            {
                throw new MyException(MyCodesNumbers.QuizNotFound, $"Quiz with id {id} not found.");
            }
            await _quizRepository.RemoveAsync(quiz);

            var media = await _quizRepository.GetMediaAsync(id);
            media.SetQuiz(null);
            if(media != null)
            {
                if(media is Course)
                {
                   await _courseRepository.UpdateAsync(media as Course);
                }
                else
                {
                    await _videoRepository.UpdateAsync(media as Video);
                }
            }
        }

        public async Task UpdateQuizAsync(UpdateQuizResource quiz)
        {
            var currentQuiz = await _quizRepository.GetAsync(quiz.QuizId);
            if(currentQuiz == null)
            {
                throw new MyException(MyCodesNumbers.QuizNotFound, $"Quiz with id {quiz.QuizId} not found.");
            }

            currentQuiz.Questions.Clear();
            foreach (var question in quiz.Questions)
            {
                var newQuestion = new QuizQuestion(question.Question);
                foreach (var answer in question.Answers)
                {
                    newQuestion.Answers.Add(new QuizAnswer(answer.Answer, answer.IsCorrect));
                }
                currentQuiz.Questions.Add(newQuestion);
            }

            await _quizRepository.UpdateAsync(currentQuiz);
        }
    }
}
