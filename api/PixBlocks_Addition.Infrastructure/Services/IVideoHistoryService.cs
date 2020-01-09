using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IVideoHistoryService
    {
        Task SetProgressAsync(Guid userId, Guid videoId, long time = 0);
        Task<VideoHistoryDto> GetHistoryAsync(Guid userId);
        Task<VideoRecordDto> GetVideoProgressAsync(Guid userId, Guid videoId);
        Task<int> GetProgressAsync(Guid userId, Guid courseId);
    }
}
