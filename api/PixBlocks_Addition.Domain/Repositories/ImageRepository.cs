using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class ImageRepository: IImageRepository
    {
        private readonly PixBlocksContext _entities;

        public ImageRepository(PixBlocksContext context)
        {
            _entities = context;
        }

        public async Task AddAsync(CustomImage image)
        {
            await _entities.Images.AddAsync(image);
            await _entities.SaveChangesAsync();
        }

        public async Task<CustomImage> GetAsync(Guid id)
        {
            return await _entities.Images.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var image = await GetAsync(id);
            _entities.Images.Remove(image);
            await _entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomImage image)
        {
            _entities.Images.Update(image);
            await _entities.SaveChangesAsync();
        }
    }
}
