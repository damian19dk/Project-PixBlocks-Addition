using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IResourceService
    {
        Task UploadAsync(IFormFile resource);
        Task<ImageDto> GetAsync(Guid id);
        Task ChangeAsync(ChangeResource resource);
        Task RemoveAsync(Guid id);
    }
}
