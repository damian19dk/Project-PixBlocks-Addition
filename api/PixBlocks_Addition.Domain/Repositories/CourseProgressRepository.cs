using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class CourseProgressRepository : ICourseProgressRepository
    {
        private readonly PixBlocksContext _entities;
        public CourseProgressRepository(PixBlocksContext entities)
        {
            _entities = entities;
        }

        public async Task AddAsync(Guid courseId, Guid userId)
        {
            var progress = new CourseProgress(courseId, userId);
            await _entities.CoursesProgresses.AddAsync(progress);
            await _entities.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseProgress>> GetAllAsync(Guid userId)
        {
            return await _entities.CoursesProgresses.Where(x => x.UserId == userId).ToListAsync();
        }
        
        public async Task<CourseProgress> GetSingleProgresAsync(Guid userId, Guid courseId)
        {
            return await _entities.CoursesProgresses.SingleOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
        }

        public async Task<IEnumerable<CourseProgress>> GetAllCourseUsers(Guid courseId)
        {
            return await _entities.CoursesProgresses.Where(x => x.CourseId == courseId).ToListAsync();
        }

        public async Task UpdatePercentageAsync(CourseProgress courseProgres)
        {
             _entities.CoursesProgresses.Update(courseProgres);
            await _entities.SaveChangesAsync();
        }
    }
}
