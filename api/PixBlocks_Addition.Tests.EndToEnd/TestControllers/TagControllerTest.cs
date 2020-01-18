using Newtonsoft.Json;
using NUnit.Framework;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System.Net.Http;
using PixBlocks_Addition.Tests.EndToEnd.Extentions;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public class TagControllerTest: BaseControllerTest
    {
        [Test]
        public async Task create_tag_should_pass_and_get_tag_should_work()
        {
            var name = "Programowanie";
            var response = await createTag(name, "Przykładowy opis", "yellow", "en");
            response.EnsureSuccessStatusCode();

            httpClient.SetLanguage("en");
            var tag = await httpClient.GetAsync<TagDto>($"api/tag?name={name}");

            Assert.IsTrue(tag != null);
        }

        [Test]
        public async Task change_tag_should_work()
        {
            var name = "Nauka jazdy";
            var response = await createTag(name, "Some desc", "red", "pl");
            response.EnsureSuccessStatusCode();
            var tag = await httpClient.GetAsync<TagDto>($"api/tag?name={name}");
            var expectedTag = new TagResource()
            {
                Name = "Nauka",
                Description = "Opis",
                FontColor = "black",
                BackgroundColor = "black",
                Language = "en"
            };

            httpClient.SetLanguage("pl");
            var res = await httpClient.PutAsync($"api/tag/{tag.Id}", new StringContent(JsonConvert.SerializeObject(expectedTag), Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();
            httpClient.SetLanguage("en");
            var getTag = await httpClient.GetAsync($"api/tag?name={expectedTag.Name}");
            var content = await getTag.Content.ReadAsStringAsync();
            var newTag = JsonConvert.DeserializeObject<TagDto>(content);

            Assert.IsTrue(newTag != null);
        }

        [Test]
        public async Task change_tag_name_when_it_is_already_taken_should_fail()
        {
            var name = "Taken tag";
            var response = await createTag(name, "Some desc", "red", "pl");
            response.EnsureSuccessStatusCode();
            httpClient.SetLanguage("pl");
            var tag = await httpClient.GetAsync<TagDto>($"api/tag?name={name}");
            var expectedTag = new TagResource()
            {
                Name = name,
                Description = "Description",
                FontColor = "black",
                BackgroundColor = "black",
                Language = "pl"
            };

            httpClient.SetLanguage("pl");
            var res = await httpClient.PutAsync($"api/tag/{tag.Id}", new StringContent(JsonConvert.SerializeObject(expectedTag), Encoding.UTF8, "application/json"));
            var r = res.Content.ReadAsStringAsync();
            Assert.IsFalse(res.IsSuccessStatusCode);
        }

        [Test]
        public async Task remove_tag_should_work()
        {
            var name = "Tag to remove";
            var response = await createTag(name, "Some desc", "red", "pl");
            response.EnsureSuccessStatusCode();
            httpClient.SetLanguage("pl");
            var tag = await httpClient.GetAsync<TagDto>($"api/tag?name={name}");
            
            var res = await httpClient.DeleteAsync($"api/tag/{tag.Id}");
            res.EnsureSuccessStatusCode();
            tag = await httpClient.GetAsync<TagDto>($"api/tag?name={name}");

            Assert.IsTrue(tag == null);
        }

        private async Task<HttpResponseMessage> createTag(string name, string description, string color, string language)
        {
            var tag = new TagResource()
            {
                Name = name,
                Description = description,
                FontColor = color,
                BackgroundColor = "black",
                Language = language
            };
            return await httpClient.PostAsync("api/tag/create", new StringContent(JsonConvert.SerializeObject(tag), Encoding.UTF8, "application/json"));
        }
    }
}
