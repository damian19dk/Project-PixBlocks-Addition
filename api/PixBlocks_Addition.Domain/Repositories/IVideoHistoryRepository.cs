using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IVideoHistoryHelperRepository
    {
        Task AddAsync(VideoHistoryHelper videoHistoryHelper);
        Task<VideoHistoryHelper> GetAsync(Video video);
        Task<IEnumerable<VideoHistoryHelper>> GetAllAsync();
        Task RemoveAsync(VideoHistoryHelper videoHistory);
        Task UpdateAsync(VideoHistoryHelper videoHistory);
    }
}
