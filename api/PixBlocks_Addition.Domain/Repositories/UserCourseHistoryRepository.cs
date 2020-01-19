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
    public class UserCourseHistoryRepository : IUserCourseHistoryRepository
    {
        private readonly PixBlocksContext _entities;
        private readonly IQueryable<UserCourseHistory> _userCourseHistories;

        public UserCourseHistoryRepository(PixBlocksContext entities)
        {
            _userCourseHistories = entities.UserCourseHistories.Include(c => c.Courses);
            _entities = entities;
        }

        public async Task AddAsync(UserCourseHistory userCourseHistory)
        {
            await _entities.UserCourseHistories.AddAsync(userCourseHistory);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserCourseHistory userCourseHistory)
        {
            _entities.UserCourseHistories.Update(userCourseHistory);
            await _entities.SaveChangesAsync();
        }

        public async Task<UserCourseHistory> GetAllAsync(User user)
        {
            return await _userCourseHistories.SingleOrDefaultAsync(x => x.User == user);
        }

        public async Task RemoveAsync(User user)
        {
            var histories = await _userCourseHistories.Where(x => x.User == user).ToListAsync();

            foreach (UserCourseHistory history in histories)
            {
                _entities.UserCourseHistories.Remove(history);
            }

            await _entities.SaveChangesAsync();
        }
    }
}
