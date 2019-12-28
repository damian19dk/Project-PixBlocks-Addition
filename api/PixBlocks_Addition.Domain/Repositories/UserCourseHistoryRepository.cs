using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
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
        public UserCourseHistoryRepository(PixBlocksContext entities)
        {
            _entities = entities;
        }

        public async Task AddAsync(UserCourseHistory userCourse)
        {
            await _entities.UserCourseHistories.AddAsync(userCourse);
            await _entities.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCourseHistory>> GetAllAsync(Guid userId) => await _entities.UserCourseHistories.Where(x => x.UserId == userId).ToListAsync();
        
        public async Task<IEnumerable<UserCourseHistory>> GetAllAsync(string login)
        {
            var user = _entities.Users.SingleOrDefault(x => x.Login == login);
            var userId = user.Id;
            return await _entities.UserCourseHistories.Where(x => x.Id == userId).ToListAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var histories = _entities.UserCourseHistories.Where(x => x.Id == userId).ToListAsync();
            var count = _entities.UserCourseHistories.Where(x => x.Id == userId).Count();
            if (count <= 0) throw new MyException(MyCodesNumbers.MissingHistory, MyCodes.MissingHistory);
            for(int i=0; i<count; i++)
            {
                 _entities.UserCourseHistories.Remove(histories.Result[i]);
            }

            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string login)
        {
            var user = _entities.Users.SingleOrDefault(x => x.Login == login);
            var userId = user.Id;

            var histories = _entities.UserCourseHistories.Where(x => x.Id == userId).ToListAsync();
            var count = _entities.UserCourseHistories.Where(x => x.Id == userId).Count();
            if (count <= 0) throw new MyException(MyCodesNumbers.MissingHistory, MyCodes.MissingHistory);
            for (int i = 0; i < count; i++)
            {
                _entities.UserCourseHistories.Remove(histories.Result[i]);
            }

            await _entities.SaveChangesAsync();
        }
    }
}
