using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class ResourceRepository: IResourceRepository
    {
        private readonly PixBlocksContext _entities;

        public ResourceRepository(PixBlocksContext context)
        {
            _entities = context;
        }

        public async Task AddAsync(CustomResource resource)
        {
            await _entities.Resources.AddAsync(resource);
            await _entities.SaveChangesAsync();
        }

        public async Task<CustomResource> GetAsync(Guid id)
        {
            return await _entities.Resources.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var resource = await GetAsync(id);
            _entities.Resources.Remove(resource);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomResource resource)
        {
            _entities.Resources.Update(resource);
            await _entities.SaveChangesAsync();
        }
    }
}
