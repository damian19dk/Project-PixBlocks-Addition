using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PixBlocksContext _entities;
        public CategoryRepository(PixBlocksContext entities)
        {
            _entities = entities;
        }
        public async Task<Category> GetAsync(Guid id) => await _entities.Categories.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Category> GetAsync(string name) => await _entities.Categories.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<Category>> GetAllAsync() => await _entities.Categories.ToListAsync();

        public async Task AddAsync(Category category)
        {
            await _entities.Categories.AddAsync(category);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string name)
        {
            var category = await GetAsync(name);
            _entities.Categories.Remove(category);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var category = await GetAsync(id);
            _entities.Categories.Remove(category);
            await _entities.SaveChangesAsync();
        }


    }
}
