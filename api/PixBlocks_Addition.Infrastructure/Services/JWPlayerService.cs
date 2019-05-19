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
        private readonly string hostcdn = "https://cdn.jwplayer.com";
        private readonly IOptions<JWPlayerOptions> _jwPlayerOptions;
        private readonly IJwtPlayerHandler _jwtPlayerHandler;

        public JWPlayerService(HttpClient client, IOptions<JWPlayerOptions> jwPlayerOptions,
                IJwtPlayerHandler jwtPlayerHandler)
        {
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
