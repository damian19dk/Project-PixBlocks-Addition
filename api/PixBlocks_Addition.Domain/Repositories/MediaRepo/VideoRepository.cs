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
        private readonly ILocalizationService _localizer;
        public string ContextLanguage { get; }

        public VideoRepository(PixBlocksContext entities, ILocalizationService localizer) : base(entities)
        {
            _entities = entities;
            _videos = entities.Videos.Include(c => c.Tags);
            _localizer = localizer;
            ContextLanguage = localizer.Language;
        }

        public async Task<Video> GetAsync(Guid id)
            => await _videos.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Video>> GetAsync(string title)
            => await _videos.Where(x => x.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                   && x.Title.Contains(title)).ToListAsync();

        public async Task<IEnumerable<Video>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _videos.Where(c => c.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                   && c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();

        public async Task<IEnumerable<Video>> GetAllAsync(int page, int count = 10)
            => await _videos.Where(v => v.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase))
                     .Skip((page - 1) * count).Take(count).ToListAsync();

        public async Task<Video> GetByMediaAsync(string mediaId)
            => await _videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.MediaId == mediaId);

        public async Task<int> CountAsync()
            => await _videos.CountAsync(x => x.Language == ContextLanguage);

        public object Clone()
        {
            return new VideoRepository((PixBlocksContext)_entities.Clone(), _localizer);
        }
    }
}
