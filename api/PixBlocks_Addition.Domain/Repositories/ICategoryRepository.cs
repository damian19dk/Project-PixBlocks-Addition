using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category> GetAsync(Guid id);
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string name);
    }
}
