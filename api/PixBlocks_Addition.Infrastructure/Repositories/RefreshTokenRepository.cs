using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Infrastructure.Models;

namespace PixBlocks_Addition.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task AddAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
