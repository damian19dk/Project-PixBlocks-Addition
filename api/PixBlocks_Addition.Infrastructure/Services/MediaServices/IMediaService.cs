using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public interface IMediaService<TDto, TResource> where TDto: class where TResource: class
    {
        Task CreateAsync(TResource resource);
        Task AddVideoAsync(UploadResource upload);
        Task<TDto> GetAsync(Guid id);
        Task<IEnumerable<TDto>> GetAsync(string title);
        Task<IEnumerable<TDto>> GetAllByTagsAsync(IEnumerable<string> tags);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<IEnumerable<TDto>> GetAllAsync(int page, int count = 10);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string title);
    }
}
