using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IImageService
    {
        Task UploadAsync(IFormFile image);
        Task<ImageDto> GetAsync(Guid id);
        Task ChangeAsync(ChangeImageResource image);
        Task RemoveAsync(Guid id);
    }
}
