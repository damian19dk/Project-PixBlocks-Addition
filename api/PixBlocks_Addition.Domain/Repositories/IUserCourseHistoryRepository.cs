using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IUserCourseHistoryRepository
    {
        Task AddAsync(UserCourseHistory userCourse);
        Task<IEnumerable<UserCourseHistory>> GetAllAsync(Guid userId);
        Task<IEnumerable<UserCourseHistory>> GetAllAsync(string login);
        Task RemoveAsync(Guid userId);
        Task RemoveAsync(string login);
    }
}
