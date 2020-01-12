using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class GenericRepository<TEntity> where TEntity : Media
    {
        private readonly PixBlocksContext _entities;

        public GenericRepository(PixBlocksContext context)
        {
            _entities = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.Set<TEntity>().AddAsync(entity);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string title)
        {
            var entity = await _entities.Set<TEntity>().SingleOrDefaultAsync(x => x.Title == title);
            _entities.Set<TEntity>().Remove(entity);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _entities.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
            _entities.Set<TEntity>().Remove(entity);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _entities.Set<TEntity>().Update(entity);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            _entities.Set<TEntity>().UpdateRange(entities);
            await _entities.SaveChangesAsync();
        }
    }
}
