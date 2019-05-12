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

        public JWPlayerService(HttpClient client, IOptions<JWPlayerOptions> jwPlayerOptions)
        {
            _jwPlayerOptions = jwPlayerOptions;
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<Playlist> GetDefaultAsync(string token)
            => await GetPlaylistAsync(_jwPlayerOptions.Value.Playlist, token);

        public async Task<Playlist> GetPlaylistAsync(string id, string token="")
        {
            if (!string.IsNullOrWhiteSpace(token))
                token = "?token=" + token;
            var response = await GetAsync(host + "/v2/playlists/" + id + token);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Playlist>(content);
            return result;
        }

        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                return response;
            }
            catch (Exception)
            {
                throw new Exception("Couldn't load the specific resource "+url);
            }
        }
    }
}
