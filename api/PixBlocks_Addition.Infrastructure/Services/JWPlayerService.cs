using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class JWPlayerService : IJWPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly IJWPlayerAuthHandler _auth;
        private readonly string hostcdn = "https://cdn.jwplayer.com";
        private readonly string hostapi = "http://api.jwplatform.com";
        private readonly IOptions<JWPlayerOptions> _jwPlayerOptions;
        private readonly IJwtPlayerHandler _jwtPlayerHandler;

        public JWPlayerService(HttpClient client, IOptions<JWPlayerOptions> jwPlayerOptions,
                IJwtPlayerHandler jwtPlayerHandler, IJWPlayerAuthHandler auth)
        {
            _auth = auth;
            _jwPlayerOptions = jwPlayerOptions;
            _jwtPlayerHandler = jwtPlayerHandler;
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<JWPlayerMedia> GetPlaylistAsync(string id)
        {
            var endpoint = "/v2/playlists/" + id;
            var result = await getMediaAsync(endpoint);
            return result;
        }

        public async Task<JWPlayerVideo> GetVideoAsync(string id)
        {
            var endpoint = "/v2/media/" + id;
            var result = await getMediaAsync(endpoint);
            return result.Videos.Single();
        }

        public async Task<JWPlayerStatus> ShowVideoAsync(string mediaId)
        {
            var endpoint = "/v1/videos/show/?";
            var dic = new Dictionary<string, string>();
            dic.Add("video_key", mediaId);
            var sig = _auth.CreateSignature(_jwPlayerOptions.Value.ApiKey, _jwPlayerOptions.Value.SecretKey, "json", dic);
            var response = await getAsync(hostapi + endpoint + sig);
            var result = await response.ReadAsStringAsync();
            result = result.Split("video\":").Last();
            result = result.Remove(result.Length - 1);
            return JsonConvert.DeserializeObject<JWPlayerStatus>(result);
        }

        public async Task<string> UploadVideoAsync(IFormFile formFile)
        {
            if(formFile == null || string.IsNullOrWhiteSpace(formFile.FileName))
            {
                throw new MyException(MyCodesNumbers.MissingFile, "Video file is missing.");
            }
            byte[] file;
            string filename = formFile.FileName;
            using (var ms = new MemoryStream())
            {
                await formFile.CopyToAsync(ms);
                file = ms.ToArray();
            }
            string address = await CreateVideoAsync();
            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");

            var fileContent = new ByteArrayContent(file);
            fileContent.Headers.Clear();
            fileContent.Headers.Add("Content-Disposition", $"form-data; name=\"file\"; filename=\"{filename}\"");
            fileContent.Headers.Add("Content-Type", "application/octet-stream");

            var multipartContent = new MultipartFormDataContent(boundary);
            multipartContent.Headers.Clear();
            multipartContent.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            multipartContent.Add(fileContent, "file", filename);

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), address))
            {
                request.Headers.ExpectContinue = true;
                request.Headers.Add("Connection", "Keep-Alive");
                request.Content = multipartContent;
                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new MyException(MyCodesNumbers.CouldNotUploadVideo, response.ReasonPhrase);
                }
                var message = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(message);
                return data.media.key;
            }
        }

        public async Task<string> CreateVideoAsync()
        {
            var endpoint = "/v1/videos/create/?";
            var sig = _auth.CreateSignature(_jwPlayerOptions.Value.ApiKey, _jwPlayerOptions.Value.SecretKey);
            var response = await getAsync(hostapi + endpoint + sig);
            var result = await response.ReadAsStringAsync();
            dynamic data = JObject.Parse(result);
            return "http://" + data.link.address + data.link.path + "?api_format=json&key=" + data.link.query.key
                + "&token=" + data.link.query.token;
        }

        public async Task DeleteVideoAsync(string id)
        {
            var endpoint = "/v1/videos/delete/?";
            var dic = new Dictionary<string, string>();
            dic.Add("video_key", id);
            var sig = _auth.CreateSignature(_jwPlayerOptions.Value.ApiKey, _jwPlayerOptions.Value.SecretKey, "json", dic);
            var response = await postAsync(hostapi + endpoint + sig, null);
        }

        private async Task<JWPlayerMedia> getMediaAsync(string endpoint)
        {
            var token = _jwtPlayerHandler.Create(endpoint);
            var response = await getAsync(hostcdn + endpoint + "?token=" + token);
            var content = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JWPlayerMedia>(content);
        }

        private async Task<HttpContent> getAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    throw new MyException(response.ReasonPhrase);
                return response.Content;
            }
            catch (Exception)
            {
                throw new MyException(MyCodesNumbers.CouldntLoad, "Nie można załadować treści " + url);
            }
        }
        private async Task<HttpContent> postAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _httpClient.PostAsync(url, content);
                var rs = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new MyException(response.ReasonPhrase);
                return response.Content;
            }
            catch (Exception)
            {
                throw new MyException(MyCodesNumbers.CouldntLoad, "Nie można załadować treści " + url);
            }
        }
    }
}