using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly IQueryable<Course> _courses;

        public CourseRepository(PixBlocksContext entities) : base(entities)
        {
            _courses = entities.Courses
                        .Include(c => c.CourseVideos).ThenInclude(p => p.Video).ThenInclude(x => x.Tags)
                        .Include(c => c.Tags);
        }

        public async Task<Course> GetAsync(Guid id)
            => await _courses.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Course>> GetAsync(string name, string language = "")
        {
            if(String.IsNullOrEmpty(language))
                return await _courses.Where(c => c.Title.Contains(name)).ToListAsync();
            else
                return await _courses.Where(c => c.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase)
                                                 && c.Title.Contains(name)).ToListAsync();
        }
            
        public async Task<IEnumerable<Course>> GetAllByTagsAsync(IEnumerable<string> tags, string language = "")
        {
            if(String.IsNullOrEmpty(language))
                return await _courses.Where(c => c.Tags.Any(t => tags.Contains(t.Name))).ToListAsync();
            else
                return await _courses.Where(c => c.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase)
                                                 && c.Tags.Any(t => tags.Contains(t.Name) && t.CheckLanguage(language)))
                                                 .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync(int page, int count = 10, string language = "")
        {
            if (String.IsNullOrEmpty(language))
            {
                return (await _courses.Skip((page - 1) * count).Take(count).OrderBy(x => x.Index).ToListAsync())
                        .Select(x => { x.CourseVideos.Sort(p => p.Video.Index); return x; });
            }
            else
            {
                return (await _courses.Where(c => c.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase))
                        .Skip((page - 1) * count).Take(count).OrderBy(x => x.Index).ToListAsync())
                        .Select(x => { x.CourseVideos.Sort(p => p.Video.Index); return x; });
            }
        }

        public async Task<int> CountAsync(string language="")
        {
            if(String.IsNullOrEmpty(language))
                return await _courses.CountAsync();
            else
                return await _courses.CountAsync(x => x.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
