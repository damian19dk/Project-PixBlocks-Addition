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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly PixBlocksContext _entities;

        public CourseRepository(PixBlocksContext entities): base(entities)
        {
            _entities = entities;
        }

        public async Task<Course> GetAsync(Guid id) 
            => await _entities.Courses.Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c=>c.Lessons).ThenInclude(x => x.Tags)
                    .Include(c=>c.Category)
                    .Include(c => c.Tags).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Course>> GetAsync(string name) 
            => await _entities.Courses.Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c => c.Lessons).ThenInclude(x => x.Tags)
                    .Include(c => c.Category)
                    .Include(c => c.Tags)
                    .Where(x => x.Title == name).ToListAsync();

        public async Task<IEnumerable<Course>> GetAllByTagsAsync(IEnumerable<string> tags)
            => await _entities.Courses.Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c => c.Lessons).ThenInclude(x => x.Tags)
                    .Include(c => c.Category)
                    .Include(c => c.Tags)
                    .Where(c => c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();

        public async Task<IEnumerable<Course>> GetAllAsync() 
            => await _entities.Courses.Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c => c.Lessons).ThenInclude(x => x.Tags)
                    .Include(c => c.Category)
                    .Include(c => c.Tags).ToListAsync();

        public async Task<IEnumerable<Course>> GetAllAsync(int page, int count = 10)
            => await _entities.Courses.Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                    .Include(c => c.Lessons).ThenInclude(x => x.Tags)
                    .Include(c => c.Category)
                    .Include(c => c.Tags).Skip((page - 1) * count).Take(count).ToListAsync();
    }
}
