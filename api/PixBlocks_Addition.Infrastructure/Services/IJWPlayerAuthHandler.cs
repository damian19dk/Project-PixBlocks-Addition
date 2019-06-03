using System.Collections.Generic;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerAuthHandler
    {
        string CreateSignature(string api_key, string secret, string format = "json", IDictionary<string, string> parameters = null);
    }
}
