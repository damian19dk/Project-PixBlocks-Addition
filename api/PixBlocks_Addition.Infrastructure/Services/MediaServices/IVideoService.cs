﻿using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public interface IVideoService
    {
        Task AddAsync(MediaResource video);
        Task<VideoDto> GetAsync(Guid id);
        Task<VideoDto> GetAsync(string mediaId);
        Task<IEnumerable<VideoDto>> BrowseAsync(string title);
        Task<IEnumerable<VideoDto>> GetAllAsync();
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string mediaId);
    }
}
