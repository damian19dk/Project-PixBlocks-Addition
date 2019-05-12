﻿using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerService
    {
        Task<Playlist> GetPlaylistAsync(string id, string token="");
        Task<Playlist> GetDefaultAsync(string token);
        
    }
}
