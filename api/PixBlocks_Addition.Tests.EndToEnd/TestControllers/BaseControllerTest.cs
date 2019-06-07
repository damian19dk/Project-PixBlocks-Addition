using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using PixBlocks_Addition.Api;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    public abstract class BaseControllerTest
    {
        protected readonly HttpClient httpClient;
        protected readonly TestServer server;

        public CourseDto CourseDto { get; set; }

        public BaseControllerTest()
        {
            var webBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration((a, c) => { c.AddJsonFile("appsettings.json"); })
                .UseStartup<Startup>();
            server = new TestServer(webBuilder);
            httpClient = server.CreateClient();
            Init().Wait();
        }

        private async Task Init()
        {
            var courseData = new Dictionary<string, string>();
            courseData.Add("Premium", "false");
            courseData.Add("Title", "Title1");
            courseData.Add("Description", "Some description.");
            courseData.Add("Language", "English");
            courseData.Add("Tags", "tag3");

            await sendMultiPartAsync("api/course/create", "POST", courseData);
            var response = await httpClient.GetAsync("api/course/all");
            var responseString = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(responseString);
            CourseDto = courses.SingleOrDefault(x => x.Title == "Title1");

            Assert.IsTrue(courses.Any(x => x.Title == "Title1"));
            Assert.IsTrue(Guid.Empty != CourseDto.Id);
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task sendMultiPartAsync(string address, string method, IDictionary<string, string> parameters,
            Models.File image = null, Models.File video = null)
        {
            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
            var multipartContent = new MultipartFormDataContent(boundary);
            multipartContent.Headers.Clear();
            multipartContent.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);

            foreach (var p in parameters)
            {
                if(p.Value != null)
                    multipartContent.Add(new StringContent(p.Value), p.Key);
                else
                    multipartContent.Add(new StringContent(string.Empty), p.Key);
            }

            if (video != null)
            {
                var fileContent = new ByteArrayContent(video.FileBytes);
                fileContent.Headers.Clear();
                fileContent.Headers.Add("Content-Disposition", $"form-data; name=\"video\"; filename=\"{video.FileName}\"");
                fileContent.Headers.Add("Content-Type", "application/octet-stream");

                multipartContent.Add(fileContent, "video", video.FileName);
            }

            if (image != null)
            {
                var fileContent = new ByteArrayContent(image.FileBytes);
                fileContent.Headers.Clear();
                fileContent.Headers.Add("Content-Disposition", $"form-data; name=\"image\"; filename=\"{image.FileName}\"");
                fileContent.Headers.Add("Content-Type", "application/octet-stream");

                multipartContent.Add(fileContent, "image", image.FileName);
            }


            using (var request = new HttpRequestMessage(new HttpMethod(method), address))
            {
                request.Headers.ExpectContinue = true;
                request.Headers.Add("Connection", "Keep-Alive");
                request.Content = multipartContent;
                var response = await httpClient.SendAsync(request);

                Assert.IsTrue(response.EnsureSuccessStatusCode().IsSuccessStatusCode);
            }
        }
    }
}
