using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Extensions;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly PixBlocksContext _entities;

        public TagRepository(PixBlocksContext context)
        {
            _entities = context;
        }

        public async Task AddAsync(Tag tag)
        {
            _entities.Tags.Add(tag);
            await _entities.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string language = "")
        {
            if (language == string.Empty)
                return await _entities.Tags.ToListAsync();
            else
                return await _entities.Tags.Where(x => x.CheckLanguage(language)).ToListAsync();
        }

        public async Task<Tag> GetAsync(string name, string language = "")
        {
            if (language == string.Empty)
                return await _entities.Tags.FirstOrDefaultAsync(x => x.Name == name);
            else
                return await _entities.Tags.SingleOrDefaultAsync(x => x.Name == name 
                                                && x.CheckLanguage(language));
        }

        public async Task<Tag> GetAsync(Guid id)
            => await _entities.Tags.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Tag>> BrowseAsync(string name, string language = "")
        {
            if (language == string.Empty)
                return await _entities.Tags.Where(x => x.Name.Contains(name)).ToListAsync();
            else
                return await _entities.Tags.Where(x => x.Name.Contains(name) 
                                                 && x.CheckLanguage(language))
                                                 .ToListAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _entities.Tags.Update(tag);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag[] tags)
        {
            _entities.Tags.UpdateRange(tags);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Tag tag)
        {
            var courses = _entities.Courses.Where(x => x.Tags.Contains(tag));
            foreach(var course in courses)
            {
                course.Tags.Remove(tag);
            }
            _entities.Courses.UpdateRange(courses);

            var videos = _entities.Videos.Where(x => x.Tags.Contains(tag));
            foreach (var video in videos)
            {
                video.Tags.Remove(tag);
            }
            _entities.Videos.UpdateRange(videos);

            _entities.Tags.Remove(tag);
            await _entities.SaveChangesAsync();
        }
    }
}
