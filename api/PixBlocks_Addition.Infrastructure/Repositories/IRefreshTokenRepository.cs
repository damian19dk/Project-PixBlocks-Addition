using PixBlocks_Addition.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
    }
}
