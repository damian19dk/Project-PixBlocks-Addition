using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        private readonly PixBlocksContext _entities;
        private readonly IQueryable<Video> _videos;

        public VideoRepository(PixBlocksContext entities) : base(entities)
        {
            _entities = entities;
            _videos = entities.Videos.Include(c => c.Tags);
        }

        public async Task<Video> GetAsync(Guid id)
            => await _videos.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Video>> GetAsync(string name, string language = "")
        {
            if (String.IsNullOrEmpty(language))
                return await _videos.Where(c => c.Title.Contains(name)).ToListAsync();
            else
                return await _videos.Where(c => c.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase)
                                                 && c.Title.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetAllByTagsAsync(IEnumerable<string> tags, string language = "")
        {
            if (String.IsNullOrEmpty(language))
                return await _videos.Where(c => c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
            else
                return await _videos.Where(c => c.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase)
                                                 && c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetAllAsync(int page, int count = 10, string language = "")
        {
            if (String.IsNullOrEmpty(language))
                return await _videos.Skip((page - 1) * count).Take(count).OrderBy(x => x.Index).ToListAsync();
            else
                return await _videos.Where(v => v.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase))
                                    .Skip((page - 1) * count).Take(count).OrderBy(x => x.Index).ToListAsync();
        }

        public async Task<Video> GetByMediaAsync(string mediaId)
            => await _videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.MediaId == mediaId);

        public object Clone()
        {
            return new VideoRepository((PixBlocksContext)_entities.Clone());
        }

        public async Task<int> CountAsync(string language = "")
        {
            if (String.IsNullOrEmpty(language))
                return await _videos.CountAsync(v => v.Language == language);
            else
                return await _videos.CountAsync();
        }
    }
}
