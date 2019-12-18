using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Tests.EndToEnd.Extentions;
using PixBlocks_Addition.Tests.EndToEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class CourseControllerTest : BaseControllerTest
    {
        [Test]
        public async Task create_course_with_same_title_should_fail()
        {
            var title = "Unique Title";
            await createCourseAsync(title, "Some description", "pl", "false");

            var response = await createCourseAsync(title, "My description", "pl", "false");
            var responseMessage = await response.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<ExceptionResponse>(responseMessage);

            Assert.False(response.IsSuccessStatusCode);
            Assert.IsTrue(message.Code == MyCodesNumbers.SameTitleCourse);
        }

        [Test]
        public async Task create_course_with_same_title_but_different_language_should_pass()
        {
            var title = "Unique Title";
            await createCourseAsync(title, "Some description", "pl", "false");

            var response = await createCourseAsync(title, "My description", "en", "false");

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task change_course_data_should_change_correct_values()
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
                Duration = CourseDto.Duration,
                CourseVideos = CourseDto.CourseVideos,
                Picture = CourseDto.Picture,
                PublishDate = CourseDto.PublishDate,
                Resources = new HashSet<string>(),
                Tags = CourseDto.Tags
            };
            var tags = string.Join(',', CourseDto.Tags);
            var parameters = expectedCourse.GetProperties();
            await sendMultiPartAsync(address, "PUT", parameters);
            var response = await httpClient.GetAsync($"api/course?id={CourseDto.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            var course = JsonConvert.DeserializeObject<CourseDto>(responseString);

            Assert.IsTrue(JsonConvert.SerializeObject(course) == JsonConvert.SerializeObject(expectedCourse));
        }

        [Test]
        public async Task create_course_should_add_correct_data()
        {
            var address = "api/course/create";
            var expectedTitle = "łąkIng Ded";
            var expectedPremium = true;
            var expectedLanguage = "pl";
            var expectedDescription = "Some expected description.";
            var expectedTags = "nauka jazdy,gumy,rtęć,inne";
            var parameters = new Dictionary<string, string>();
            parameters.Add("Title", expectedTitle);
            parameters.Add("Description", expectedDescription);
            parameters.Add("Language", expectedLanguage);
            parameters.Add("Premium", expectedPremium.ToString());
            parameters.Add("Tags", expectedTags);

            await sendMultiPartAsync(address, "POST", parameters);

            httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "pl");

            var response = await httpClient.GetAsync($"api/course/title?title={expectedTitle}");
            var responseString = await response.Content.ReadAsStringAsync();
            var course = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(responseString).Single();

            Assert.IsTrue(checkCourseData(parameters, course));
        }

        private bool checkCourseData(IDictionary<string, string> data, CourseDto course)
        {
            foreach (var p in data)
            {
                if (p.Key.ToLowerInvariant() != "tags")
                {
                    if (p.Value != course.GetType().GetProperty(p.Key).GetValue(course).ToString())
                        return false;
                }
                else
                {
                    foreach (var tag in course.Tags)
                    {
                        if (!data["Tags"].Contains(tag))
                            return false;
                    }
                }
            }
            return true;
        }

        private async Task<HttpResponseMessage> createCourseAsync(string title, string description, string language, string premium)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Title", title);
            parameters.Add("Description", description);
            parameters.Add("Language", language);
            parameters.Add("Premium", premium);

            return await sendMultiPartWithResponseAsync("api/course/create", "POST", parameters);
        }
    }
}
