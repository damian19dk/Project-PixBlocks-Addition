using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IVideoHistoryService
    {
        Task AddAsync(Guid userId, Guid videoId, long time = 0);
        Task UpdateAsync(Guid userId, Guid videoId, long time = 0);
        Task<VideoHistoryDto> GetUserVideoHistory(Guid userId);
        Task<int> GetUserProgres(Guid userId, Guid courseId);
    }
}
