using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class JWPlayerService: IJWPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly string host = "https://cdn.jwplayer.com";
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

        public async Task<Media> GetPlaylistAsync(string id)
        {
            var endpoint = "/v2/playlists/" + id;
            var token = _jwtPlayerHandler.Create(endpoint);
            var response = await GetAsync(host + endpoint + "?token=" + token);
            var content = await response.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Media>(content);
            return result;
        }

        private async Task<HttpContent> GetAsync(string url)
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
                throw new Exception("Couldn't load the specific resource "+url);
            }
        }
    }
}
