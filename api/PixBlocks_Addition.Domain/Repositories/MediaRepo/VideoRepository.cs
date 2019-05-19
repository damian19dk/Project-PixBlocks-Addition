using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public class VideoRepository: GenericRepository<Video>, IVideoRepository
    {
        private readonly PixBlocksContext _entities;
        public VideoRepository(PixBlocksContext entities): base(entities)
        {
            _entities = entities;
        }
        public async Task<Video> GetAsync(Guid id) => await _entities.Videos.Include(c=>c.Tags).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Video> GetAsync(string title) => await _entities.Videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.Title == title);

        public async Task<IEnumerable<Video>> GetAllAsync() => await _entities.Videos.Include(p=>p.Tags).ToListAsync();

        public async Task<Video> GetByMediaAsync(string mediaId)
            => await _entities.Videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.MediaId == mediaId);
    }
}
