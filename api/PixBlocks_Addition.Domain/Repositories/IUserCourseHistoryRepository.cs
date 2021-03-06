﻿using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IUserCourseHistoryRepository
    {
        Task AddAsync(UserCourseHistory userCourse);
        Task UpdateAsync(UserCourseHistory userCourseHistory);
        Task<UserCourseHistory> GetAllAsync(User user);
        Task RemoveAsync(User user);
    }
}
