using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IVideoHistoryHelperRepository
    {
        Task AddAsync(VideoRecord videoHistoryHelper);
        Task<VideoRecord> GetAsync(Video video);
        Task<IEnumerable<VideoRecord>> GetAllAsync();
        Task RemoveAsync(VideoRecord videoHistory);
        Task UpdateAsync(VideoRecord videoHistory);
    }
}
