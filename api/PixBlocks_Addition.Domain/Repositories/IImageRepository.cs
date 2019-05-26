using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IImageRepository
    {
        Task AddAsync(CustomImage image);
        Task<CustomImage> GetAsync(Guid id);
        Task UpdateAsync(CustomImage image);
        Task RemoveAsync(Guid id);
    }
}
