using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IResourceRepository
    {
        Task AddAsync(CustomResource resource);
        Task<CustomResource> GetAsync(Guid id);
        Task UpdateAsync(CustomResource resource);
        Task RemoveAsync(Guid id);
    }
}
