using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerAuthHandler
    {
        string CreateSignature(string api_key, string secret, string format = "json");
    }
}
