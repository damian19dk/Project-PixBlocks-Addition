using PixBlocks_Addition.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerService
    {
        Task<Playlist> GetPlaylistAsync(string id, string token="");
        Task<Playlist> GetDefaultAsync(string token);
    }
}
