﻿using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerService
    {
        Task<JWPlayerMedia> GetPlaylistAsync(string id);
        Task<JWPlayerVideo> GetVideoAsync(string id);
        Task<JWPlayerStatus> ShowVideoAsync(string mediaId);
        Task DeleteVideoAsync(string id);
        Task<string> CreateVideoAsync();
        Task<string> UploadVideoAsync(IFormFile formFile);
    }
}
