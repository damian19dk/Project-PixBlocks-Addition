using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task RemoveAsync(string name);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task<int> CountAsync();
    }
}
