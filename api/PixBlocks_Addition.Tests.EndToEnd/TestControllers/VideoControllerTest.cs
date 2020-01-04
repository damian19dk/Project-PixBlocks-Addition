using Newtonsoft.Json;
using NUnit.Framework;
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
    public class VideoControllerTest: BaseControllerTest
    {
        private static ICollection<string> jwPlayerVideos = new HashSet<string>();

        [Test]
        public async Task create_video_should_add_correct_data_and_add_video_to_specific_course()
        {
            var parentCourseId = (await httpClient.GetAsync<IEnumerable<CourseDto>>("api/course/all?page=1")).Single().Id;
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

            await createVideoAsync(expectedTitle, expectedDescription, expectedLanguage, expectedPremium, parentCourseId, null, expectedTags);
            var course = await httpClient.GetAsync<CourseDto>($"api/course?id={parentCourseId}");
            httpClient.SetLanguage("pl");
            var video = (await httpClient.GetAsync<IEnumerable<VideoDto>>($"api/video/title?title={expectedTitle}")).Single();

            Assert.IsTrue(checkVideoData(parameters, video));
            Assert.IsTrue(course.CourseVideos.Any(x => x.Title == expectedTitle));
        }

        [Test]
        public async Task remove_video_from_course_should_remove_it_from_course_and_database()
        {
            await createCourseAsync("Testowy Kurs do usuwania wideo", "przykładowy opis", "pl", false);
            var parentCourseId = (await httpClient.GetAsync<IEnumerable<CourseDto>>("api/course/all?page=1")).First().Id;
            var title = "Testowe wideo 1";
            var premium = false;
            var language = "pl";
            var description = "Some expected description.";

            await createVideoAsync(title, description, language, premium, parentCourseId);
            httpClient.SetLanguage("pl");
            var course = await httpClient.GetAsync<CourseDto>($"api/course?id={parentCourseId}");
            var videoId = course.CourseVideos.Single(x => x.Title == title).Id;
            var response = await httpClient.DeleteAsync($"api/course/video?courseId={parentCourseId}&videoId={videoId}");
            var videoResponse = await httpClient.GetAsync<VideoDto>($"api/video?id={videoId}");
            course = await httpClient.GetAsync<CourseDto>($"api/course?id={parentCourseId}");

            response.EnsureSuccessStatusCode();
            Assert.True(videoResponse == null);
        }
        private bool checkVideoData(IDictionary<string, string> data, VideoDto video)
        {
            foreach (var p in data)
            {
                if (p.Key.ToLowerInvariant() != "tags")
                {
                    if (p.Value != video.GetType().GetProperty(p.Key).GetValue(video).ToString())
                        return false;
                }
                else
                {
                    foreach (var tag in video.Tags)
                    {
                        if (!data["Tags"].Contains(tag))
                            return false;
                    }
                }
            }
            return true;
        }

        private async Task<HttpResponseMessage> createVideoAsync(string title, string description, string language, bool premium, Guid parentId, File file = null, string tags = null)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Title", title);
            parameters.Add("Description", description);
            parameters.Add("Language", language);
            parameters.Add("Premium", premium.ToString());
            parameters.Add("ParentId", parentId.ToString());
            if(tags != null)
            {
                parameters.Add("Tags", tags);
            }

            if (file == null)
            {
                file = new File()
                {
                    FileName = Guid.NewGuid().ToString("n").Substring(0, 8),
                    FileBytes = System.IO.File.ReadAllBytes("Assets/ten.mp4")
                };
            }

            var response = await sendMultiPartWithResponseAsync("api/video/create", "POST", parameters, null, file);
            if(response.IsSuccessStatusCode)
            {
                var video = (await httpClient.GetAsync<CourseDto>($"api/course?id={parentId}")).CourseVideos.Single(x=>x.Title==title);
                jwPlayerVideos.Add(video.MediaId);
            }

            return response;
        }
    }
}
