﻿using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class VideoHistoryHelperRepository : IVideoHistoryHelperRepository
    {
        private readonly PixBlocksContext _entities;
        private readonly IQueryable<VideoHistoryHelper> _videoHistoryHelpers;

        public VideoHistoryHelperRepository(PixBlocksContext entities)
        {
            _entities = entities;
            _videoHistoryHelpers = entities.VideoHistoryHelpers;
        }

        public async Task AddAsync(VideoHistoryHelper videoHistoryHelper)
        {
            await _entities.VideoHistoryHelpers.AddAsync(videoHistoryHelper);
            await _entities.SaveChangesAsync();
        }

        public async Task<VideoHistoryHelper> GetAsync(Video video) => await _videoHistoryHelpers.SingleOrDefaultAsync(x => x.Video == video);

        public async Task<IEnumerable<VideoHistoryHelper>> GetAllAsync() => await _videoHistoryHelpers.ToListAsync();

        public async Task RemoveAsync(VideoHistoryHelper videoHistoryHelper)
        {
            _entities.VideoHistoryHelpers.Remove(videoHistoryHelper);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(VideoHistoryHelper videoHistoryHelper)
        {
            _entities.VideoHistoryHelpers.Update(videoHistoryHelper);
            await _entities.SaveChangesAsync();
        }
    }
}
