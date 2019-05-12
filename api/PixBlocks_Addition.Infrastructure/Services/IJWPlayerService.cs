using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IJWPlayerService
    {
        Task<Media> GetPlaylistAsync(string id);
        Task<Video> GetVideoAsync(string id);
    }
}
