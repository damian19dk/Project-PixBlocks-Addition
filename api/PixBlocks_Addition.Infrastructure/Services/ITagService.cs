using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface ITagService
    {
        Task CreateAsync(TagResource tag);
        Task<TagDto> GetAsync(string name);
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<IEnumerable<TagDto>> BrowseAsync(string name);
        Task UpdateAsync(string name, TagResource tag);
        Task RemoveAsync(string name);
    }
}
