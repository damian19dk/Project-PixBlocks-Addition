using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task<RefreshToken> GetByUserIdAsync(Guid userId);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync();
    }
}
