using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
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
                    throw new Exception(response.ReasonPhrase);
                return response.Content;
            }
            catch (Exception)
            {
                throw new Exception("Couldn't load the specific resource " + url);
            }
        }
    }
}