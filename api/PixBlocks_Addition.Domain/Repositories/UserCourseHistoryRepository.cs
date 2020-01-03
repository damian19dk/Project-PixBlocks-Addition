﻿using Microsoft.EntityFrameworkCore;
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
        private readonly IUserRepository _userRepository;
        private readonly IQueryable<UserCourseHistory> _userCourseHistories;

        public UserCourseHistoryRepository(PixBlocksContext entities, IUserRepository userRepository)
        {
            _userCourseHistories = entities.UserCourseHistories.Include(c => c.Courses);
            _entities = entities;
            _userRepository = userRepository;
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

        public async Task<UserCourseHistory> GetAllAsync(Guid userId)
        {
            return await _userCourseHistories.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<UserCourseHistory> GetAllAsync(string login)
        {
            var user = await _userRepository.GetAsync(login);
            var userId = user.Id;


            return await _userCourseHistories.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task RemoveAsync(Guid userId)
        {
            var histories = await _userCourseHistories.Where(x => x.UserId == userId).ToListAsync();

            foreach (UserCourseHistory history in histories)
            {
                _entities.UserCourseHistories.Remove(history);
            }

            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string login)
        {
            var user = _entities.Users.SingleOrDefault(x => x.Login == login);
            var userId = user.Id;

            var histories = await _entities.UserCourseHistories.Where(x => x.UserId == userId).ToListAsync();

            foreach(UserCourseHistory history in histories)
            {
                _entities.UserCourseHistories.Remove(history);
            }

            await _entities.SaveChangesAsync();
        }
    }
}
