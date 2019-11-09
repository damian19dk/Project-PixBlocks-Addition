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
        private readonly ILocalizationService _localizer;
        public string ContextLanguage { get; }

        public VideoRepository(PixBlocksContext entities, ILocalizationService localizer) : base(entities)
        {
            _entities = entities;
            _localizer = localizer;
            ContextLanguage = localizer.Language;
        }

        public async Task<Video> GetAsync(Guid id)
            => await _entities.Videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Video>> GetAsync(string title)
            => await _entities.Videos.Include(c => c.Tags)
                     .Where(x => x.Language == ContextLanguage &&  x.Title == title).ToListAsync();

        public async Task<IEnumerable<Video>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _entities.Videos.Where(c => c.Language == ContextLanguage && c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();

        public async Task<IEnumerable<Video>> GetAllAsync(int page, int count = 10) 
            => await _entities.Videos.Include(p => p.Tags)
                     .Where(v => v.Language == ContextLanguage)
                     .Skip((page - 1) * count).Take(count).ToListAsync();

        public async Task<Video> GetByMediaAsync(string mediaId)
            => await _entities.Videos.Include(c => c.Tags).SingleOrDefaultAsync(x => x.MediaId == mediaId);

        public object Clone()
        {
            return new VideoRepository((PixBlocksContext)_entities.Clone(), _localizer);
        }
    }
}
