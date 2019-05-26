using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class ImageHandler : IImageHandler
    {
        public ImageHandler()
        {

        }

        public ImageDto Convert(CustomImage image)
        {
            var dto = new ImageDto();
            dto.ContentType = image.ContentType;
            dto.Image = System.Convert.FromBase64String(image.Image);
            return dto;
        }

        public async Task<CustomImage> CreateAsync(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                var base64 = System.Convert.ToBase64String(memoryStream.ToArray());
                return CustomImage.Create(image.ContentType, base64);
            }
        }
    }
}
