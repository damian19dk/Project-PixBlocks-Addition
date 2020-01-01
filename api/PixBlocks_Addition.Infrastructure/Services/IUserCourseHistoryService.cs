using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IUserCourseHistoryService
    {
        Task AddHistoryAsync(Guid userId, Guid courseId);
        Task AddHistoryAsync(string login, Guid courseId);
        Task<IEnumerable<CourseDto>> GetAllAsync(Guid userId);
        Task<IEnumerable<CourseDto>> GetAllAsync(string login);
        Task RemoveAsync(Guid userId);
        Task RemoveAsync(string login);
    }
}
