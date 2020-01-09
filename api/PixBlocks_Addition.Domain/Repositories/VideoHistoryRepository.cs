using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class VideoHistoryRepository : IVideoHistoryRepository
    {
        private readonly PixBlocksContext _entities;
        private readonly IQueryable<VideoHistory> _videoHistories;


        public VideoHistoryRepository(PixBlocksContext entities)
        {
            _videoHistories = entities.VideoHistories.Include(x => x.Videos);
            _entities = entities;
        }

        public async Task AddAsync(VideoHistory videoHistory)
        {
            await _entities.VideoHistories.AddAsync(videoHistory);
            await _entities.SaveChangesAsync();
        }

        public async Task<VideoHistory> GetAsync(User user) => await _videoHistories.SingleOrDefaultAsync(x => x.User == user);

        public async Task<IEnumerable<VideoHistory>> GetAllAsync() => await _videoHistories.ToListAsync();

        public async Task RemoveAsync(VideoHistory videoHistory)
        {
            _entities.VideoHistoryHelpers.RemoveRange(videoHistory.Videos);
            _entities.VideoHistories.Remove(videoHistory);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(VideoHistory videoHistory)
        {
            _entities.VideoHistories.Update(videoHistory);
            await _entities.SaveChangesAsync();
        }
    }
}
