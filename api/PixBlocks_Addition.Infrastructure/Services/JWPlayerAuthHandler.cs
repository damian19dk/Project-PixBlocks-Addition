using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PixBlocks_Addition.Infrastructure.Extentions;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class JWPlayerAuthHandler : IJWPlayerAuthHandler
    {
        private Random random = new Random();

        public JWPlayerAuthHandler()
        {

        }

        public string CreateSignature(string api_key, string secret, string format = "json", IDictionary<string, string> parameters = null)
        {
            var nonce = "171" + random.Next(10000, 99999);
            var now = DateTime.UtcNow.ToTimestamp().ToString();
            var payload = new Dictionary<string, string>();
            if (parameters != null)
            {
                foreach (var value in parameters)
                    payload.Add(value.Key, value.Value);
            }
            payload.Add("api_format", format);
            payload.Add("api_key", api_key);
            payload.Add("api_nonce", nonce);
            payload.Add("api_timestamp", now);
            StringBuilder sb = new StringBuilder();
            foreach (var value in payload.OrderBy(v => v.Key))
                sb.Append(value.Key).Append("=").Append(value.Value).Append("&");
            sb.Remove(sb.Length - 1, 1);

            var data = sb.ToString();
            var sig = getHash(data + secret);
            return data + "&api_signature=" + sig;
        }

        private string getHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
