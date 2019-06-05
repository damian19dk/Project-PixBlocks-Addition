using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class CourseControllerTest: BaseControllerTest
    {
        [Test]
        public async Task change_course_data()
        {
            var address = "api/course/change";
            var expectedTitle = "Linda";
            var expectedPremium = true;
            var expectedLanguage = "PL";
            var expectedDescription = "Some expected description.";
            var expectedCourse = new CourseDto()
            {
                Title = expectedTitle,
                Description = expectedDescription,
                Language = expectedLanguage,
                Premium = expectedPremium,
                Id = CourseDto.Id,
                MediaId = CourseDto.MediaId,
                Category = CourseDto.Category,
                CourseVideos = CourseDto.CourseVideos,
                Duration = CourseDto.Duration,
                Lessons = CourseDto.Lessons,
                Picture = CourseDto.Picture,
                PublishDate = CourseDto.PublishDate,
                Tags = CourseDto.Tags
            };
            var parameters = new Dictionary<string, string>();
            parameters.Add("id", CourseDto.Id.ToString());
            parameters.Add("title", expectedTitle);
            parameters.Add("language", expectedLanguage);
            parameters.Add("premium", expectedPremium.ToString());
            parameters.Add("description", expectedDescription);

            await sendMultiPartAsync(address, "PUT", parameters);
            var response = await httpClient.GetAsync($"api/course?id={CourseDto.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var course = JsonConvert.DeserializeObject<CourseDto>(responseString);
            Assert.IsTrue(JsonConvert.SerializeObject(course) == JsonConvert.SerializeObject(expectedCourse));
        }
    }
}
