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
    public class LessonRepository: GenericRepository<Lesson>, ILessonRepository
    {
        private readonly PixBlocksContext _entities;

        public LessonRepository(PixBlocksContext context): base(context)
        {
            _entities = context;
        }

        public async Task<Lesson> GetAsync(Guid id) 
            => await _entities.Lessons.Include(c => c.LessonVideos).ThenInclude(p => p.Video).ThenInclude(x=>x.Tags)
                     .Include(c => c.Tags)
                     .Include(c=>c.Exercises).ThenInclude(x => x.Tags)
                     .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Lesson>> GetAsync(string name)
            => await _entities.Lessons.Include(c => c.LessonVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Include(c => c.Exercises).ThenInclude(x => x.Tags)
                     .Where(x => x.Title.Contains(name)).ToListAsync();

        public async Task<IEnumerable<Lesson>> GetAllAsync(int page, int count = 10)
            => await _entities.Lessons.Include(c => c.LessonVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Include(c => c.Exercises).ThenInclude(x => x.Tags).Skip((page -1)*count).Take(count)
                     .ToListAsync();

        public async Task<IEnumerable<Lesson>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _entities.Lessons.Include(c => c.LessonVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                     .Include(c => c.Tags)
                     .Include(c => c.Exercises).ThenInclude(x => x.Tags)
                     .Where(c => c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
    }
}
