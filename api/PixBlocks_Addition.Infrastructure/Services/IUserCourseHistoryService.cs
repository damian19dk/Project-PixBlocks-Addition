using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IUserCourseHistoryService
    {
        Task AddHistoryAsync(Guid userId, Course course);
        Task AddHistoryAsync(string login, Course course);
        Task<IEnumerable<UserCourseHistoryDto>> GetAllAsync(Guid userId);
        Task<IEnumerable<UserCourseHistoryDto>> GetAllAsync(string login);
        Task RemoveAsync(Guid userId);
        Task RemoveAsync(string login);
    }
}
