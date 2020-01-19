using Newtonsoft.Json;
using NUnit.Framework;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Tests.EndToEnd.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class OrderControllerTest: BaseControllerTest
    {
        [Test]
        public async Task permutation_courses_should_order_courses()
        {
            var address = "api/order/courses";
            var createCourseEndpoint = "api/course/create";
            httpClient.SetLanguage("en");

            var course1 = new MediaResource()
            {
                Title = "Test course 1",
                Language = "en",
                Premium = false,
                Description = "Something like this"
            };
            var course2 = new MediaResource()
            {
                Title = "Test course 2",
                Language = "en",
                Premium = false,
                Description = "Something like this"
            };
            var course3 = new MediaResource()
            {
                Title = "Test course 3",
                Language = "en",
                Premium = false,
                Description = "Something like this"
            };
            var course4 = new MediaResource()
            {
                Title = "Test course 4",
                Language = "pl",
                Premium = false,
                Description = "Something like this"
            };

            await sendMultiPartAsync(createCourseEndpoint, "POST", course1.GetProperties(x => new { x.Title, x.Language, x.Premium, x.Description }));
            await sendMultiPartAsync(createCourseEndpoint, "POST", course2.GetProperties(x => new { x.Title, x.Language, x.Premium, x.Description }));
            await sendMultiPartAsync(createCourseEndpoint, "POST", course3.GetProperties(x => new { x.Title, x.Language, x.Premium, x.Description }));
            await sendMultiPartAsync(createCourseEndpoint, "POST", course4.GetProperties(x => new { x.Title, x.Language, x.Premium, x.Description }));

            var courses = await httpClient.GetAsync<IEnumerable<CourseDto>>($"api/course/all");

            List<Guid> coursesId = new List<Guid>();
            IEnumerable<Guid> expectedCoursesId = new List<Guid>();

            foreach (var course in courses)
            {
                coursesId.Add(course.Id);
            }

            expectedCoursesId = (coursesId as IEnumerable<Guid>).Reverse();
            var data = new StringContent(JsonConvert.SerializeObject(new OrderCoursesResource() { Courses = expectedCoursesId }), Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(address, data);
            var r = await res.Content.ReadAsStringAsync();

            var newCourses = await httpClient.GetAsync<IEnumerable<CourseDto>>("api/course/all");

            foreach (var values in newCourses.Zip(expectedCoursesId, (course, expectedId) => new { CourseId = course.Id, ExpectedId = expectedId }))
            {
                Assert.IsTrue(values.CourseId == values.ExpectedId);
            }
        }
    }
}
