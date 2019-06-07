using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Tests.EndToEnd.Extentions;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class LessonControllerTest : BaseControllerTest
    {
        public LessonDto LessonDto { get; set; }

        public LessonControllerTest()
        {
            Init().Wait();
        }

        private async Task Init()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Title", "LessonTitle1");
            parameters.Add("Description", "Description lesson.");
            parameters.Add("Premium", "false");
            parameters.Add("Language", "English");
            parameters.Add("ParentId", CourseDto.Id.ToString());

            sendMultiPartAsync("api/lesson/create", "POST", parameters).Wait();

            var response = await httpClient.GetAsync("api/lesson/title?title=LessonTitle1");
            var responseString = await response.Content.ReadAsStringAsync();
            var lessons = JsonConvert.DeserializeObject<IEnumerable<LessonDto>>(responseString);
            LessonDto = lessons.Single();
        }

        [Test]
        public async Task change_lesson_data_should_change_correct_values()
        {
            var address = "api/lesson/change";
            var expectedTitle = "expected changed title";
            var expectedPremium = true;
            var expectedLanguage = "PL";
            var expectedDescription = "Some expected description.";
            var expectedTags = "Granie na fortepianie,inne";
            var expectedLesson = new LessonDto()
            {
                Title = expectedTitle,
                Description = expectedDescription,
                Language = expectedLanguage,
                Premium = expectedPremium,
                Id = LessonDto.Id,
                MediaId = LessonDto.MediaId,
                Duration = LessonDto.Duration,
                LessonVideos = LessonDto.LessonVideos,
                Exercises = LessonDto.Exercises,
                Picture = LessonDto.Picture,
                PublishDate = LessonDto.PublishDate,
                Tags = expectedTags.Split(',')
            };
            var parameters = expectedLesson.GetProperties(x
                => new
                {
                    x.Title,
                    x.Description,
                    x.Language,
                    x.Premium,
                    x.Id,
                    x.Duration,
                    x.Picture,
                    x.Tags
                });

            await sendMultiPartAsync(address, "PUT", parameters);
            var response = await httpClient.GetAsync($"api/lesson?id={LessonDto.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var lesson = JsonConvert.DeserializeObject<LessonDto>(responseString);

            Assert.IsTrue(JsonConvert.SerializeObject(lesson) == JsonConvert.SerializeObject(expectedLesson));
        }
    }
}
