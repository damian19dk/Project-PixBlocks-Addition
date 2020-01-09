using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IVideoHistoryRepository
    {
        Task AddAsync(VideoHistory videoHistory);
        Task<VideoHistory> GetAsync(User user);
        Task<IEnumerable<VideoHistory>> GetAllAsync();
        Task RemoveAsync(VideoHistory videoHistory);
        Task UpdateAsync(VideoHistory videoHistory);
    }
}
