using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Tests.EndToEnd.Extentions
{
    public static class HttpClientExtensions
    {
        public static void SetLanguage(this HttpClient httpClient, string language)
        {
            httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
        }

        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string address, bool ensureSuccessStatusCode = true)
        {
            var response = await httpClient.GetAsync(address);
            if (ensureSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseString);

            return result;
        }
    }
}
