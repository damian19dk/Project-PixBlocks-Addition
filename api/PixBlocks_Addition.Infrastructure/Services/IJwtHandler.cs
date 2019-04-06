using PixBlocks_Addition.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId, string login, string role,
            IDictionary<string, string> claims = null);
    }
}
