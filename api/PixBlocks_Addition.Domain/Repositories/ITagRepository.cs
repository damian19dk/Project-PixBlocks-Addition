using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        Task<Tag> GetAsync(Guid id);
        Task<Tag> GetAsync(string name);
        Task<IEnumerable<Tag>> BrowseAsync(string name);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task UpdateAsync(Tag tag);
        Task UpdateAsync(Tag[] tag);
        Task RemoveAsync(Tag tag);
    }
}
