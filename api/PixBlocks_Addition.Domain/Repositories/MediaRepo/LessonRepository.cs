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
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly IQueryable<Lesson> _lessons;
        public string ContextLanguage { get; }

        public LessonRepository(PixBlocksContext context, ILocalizationService localizer) : base(context)
        {
            _lessons = context.Lessons.Include(c => c.LessonVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Include(c => c.Exercises).ThenInclude(x => x.Tags);
            ContextLanguage = localizer.Language;
        }

        public async Task<Lesson> GetAsync(Guid id)
            => await _lessons.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Lesson>> GetAsync(string name)
            => await _lessons.Where(x => x.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                    && x.Title.Contains(name)).ToListAsync();

        public async Task<IEnumerable<Lesson>> GetAllAsync(int page, int count = 10)
            => await _lessons.Where(x => x.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase))
                     .Skip((page - 1) * count).Take(count)
                     .ToListAsync();

        public async Task<IEnumerable<Lesson>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _lessons.Where(c => c.Language.Equals(ContextLanguage, StringComparison.InvariantCultureIgnoreCase) 
                                    && c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
    }
}
