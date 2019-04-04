using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly PixBlocksContext _entities;
        public CourseRepository(PixBlocksContext entities)
        {
            _entities = entities;
        }
        public async Task<Course> GetAsync(Guid id) => await _entities.Courses.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Course> GetAsync(string name) => await _entities.Courses.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<Course>> GetAllAsync() => await _entities.Courses.ToListAsync();

        public async Task AddAsync(Course course)
        {
            await _entities.Courses.AddAsync(course);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string name)
        {
            var course = await GetAsync(name);
            _entities.Courses.Remove(course);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var course = await GetAsync(id);
            _entities.Courses.Remove(course);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _entities.Courses.Update(course);
            await _entities.SaveChangesAsync();
        }
    }
}
