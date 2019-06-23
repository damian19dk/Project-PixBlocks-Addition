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
            var parameters = expectedLesson.GetProperties();

            await sendMultiPartAsync(address, "PUT", parameters);
            var response = await httpClient.GetAsync($"api/lesson?id={LessonDto.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var lesson = JsonConvert.DeserializeObject<LessonDto>(responseString);

            Assert.IsTrue(JsonConvert.SerializeObject(lesson) == JsonConvert.SerializeObject(expectedLesson));
        }

        [Test]
        public async Task get_from_wrong_name_should_return_empty()
        {
            var wrongTitle = "Cos innego";
            var response = await httpClient.GetAsync($"api/lesson/title?/title={wrongTitle}");
            var responseString = await response.Content.ReadAsStringAsync();


            Assert.IsTrue(responseString.Contains("[]"));
        }

        [Test]
        public async Task get_from_good_name_should_return_lesson()
        {
            var goodtitle = "LessonTitle1";

            var expectedTitle = "LessonTitle1";
            var expectedPremium = false;
            var expectedLanguage = "English";
            var expectedDescription = "Description lesson.";
            var expectedTags = "Granie na fortepianie,inne";

            var parameters = new Dictionary<string, string>();
            parameters.Add("Title", "LessonTitle1");
            parameters.Add("Description", "Description lesson.");
            parameters.Add("Premium", "false");
            parameters.Add("Language", "English");
            parameters.Add("ParentId", CourseDto.Id.ToString());

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
                //PublishDate = LessonDto.PublishDate,
                Tags = expectedTags.Split(',')
            };
            


            sendMultiPartAsync("api/lesson/create", "POST", parameters).Wait();
            var response = await httpClient.GetAsync($"api/lesson?title={expectedTitle}");
            var responseString = await response.Content.ReadAsStringAsync();
            var lesson = JsonConvert.DeserializeObject<LessonDto>(responseString);
           
            Assert.IsTrue(JsonConvert.SerializeObject(lesson).ToLower() == JsonConvert.SerializeObject(expectedLesson).ToLower());
        }

        private bool checkLessonData(IDictionary<string, string> data, LessonDto lesson)
        {
            foreach (var p in data)
            {
                if (p.Key.ToLowerInvariant() != "tags")
                {
                    if (p.Value != lesson.GetType().GetProperty(p.Key).GetValue(lesson).ToString())
                        return false;
                }
                else
                {
                    foreach (var tag in lesson.Tags)
                    {
                        if (!data["Tags"].Contains(tag))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
