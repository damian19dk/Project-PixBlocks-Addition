using System;
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

        public string CreateSignature(string api_key, string secret, string format = "json")
        {
            var nonce = "171" + random.Next(10000, 99999);
            var now = DateTime.UtcNow.ToTimestamp().ToString();
            var data = "api_format=" + format + "&api_key=" + api_key + "&api_nonce=" + nonce + "&api_timestamp=" + now;
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
