using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface ICourseProgressRepository
    {
        Task AddAsync(Guid courseId, Guid userId);
        Task<IEnumerable<CourseProgress>> GetAllAsync(Guid userId);
        Task<CourseProgress> GetSingleProgresAsync(Guid userId, Guid courseId);
        Task<IEnumerable<CourseProgress>> GetAllCourseUsers(Guid courseId);
        Task UpdatePercentageAsync(CourseProgress courseProgres);
    }
}
