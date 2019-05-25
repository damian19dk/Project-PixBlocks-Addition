﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public interface IMediaRepository<T> where T: class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync(string title);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(int page, int count = 10);
        Task AddAsync(T entity);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string name);
        Task UpdateAsync(T entity);
    }
}
