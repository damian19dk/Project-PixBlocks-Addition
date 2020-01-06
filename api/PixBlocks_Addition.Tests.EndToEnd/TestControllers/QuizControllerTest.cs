using NUnit.Framework;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes;
using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Tests.EndToEnd.Extentions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class QuizControllerTest: BaseControllerTest
    {
        [Test]
        public async Task create_quiz_should_pass()
        {
            var courseTitle = "Course-with-quiz";
            var responseCourse = await createCourseAsync(courseTitle, "Quizzes are fun", "pl", false);
            responseCourse.EnsureSuccessStatusCode();
            httpClient.SetLanguage("pl");
            var course = (await httpClient.GetAsync<IEnumerable<CourseDto>>($"api/course/title?title={courseTitle}")).First();
            var quiz = new CreateQuizResource()
            {
                MediaId = course.Id,
                Questions = new List<QuizQuestionResource>()
                {
                    new QuizQuestionResource()
                    {
                        Question = "Ile mam lat ?",
                        Answers = new List<QuizAnswerResource>()
                        {
                            new QuizAnswerResource() { Answer = "Dwanaście :)", IsCorrect = false },
                            new QuizAnswerResource() { Answer = "Dwadzieścia trzy", IsCorrect = true },
                            new QuizAnswerResource() { Answer = "15", IsCorrect = false }
                        }
                    },
                    new QuizQuestionResource()
                    {
                        Question = "Pytanie testowe",
                        Answers = new List<QuizAnswerResource>()
                        {
                            new QuizAnswerResource() { Answer = "Odpowiedź numer 1", IsCorrect = false },
                            new QuizAnswerResource() { Answer = "Odpowiedź numer 2", IsCorrect = true },
                            new QuizAnswerResource() { Answer = "Odpowiedź numer 3", IsCorrect = false },
                            new QuizAnswerResource() { Answer = "Odpowiedź numer 4", IsCorrect = true }
                        }
                    }
                }
            };
            var json = JsonConvert.SerializeObject(quiz);

            var response = await httpClient.PostAsync("api/quiz/create", new StringContent(json, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
