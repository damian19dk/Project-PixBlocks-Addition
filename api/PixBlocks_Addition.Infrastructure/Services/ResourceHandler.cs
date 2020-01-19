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
    public class ResourceHandler : IResourceHandler
    {
        public ResourceHandler()
        {

        }

        public ResourceDto Convert(CustomResource resource)
        {
            var dto = new ResourceDto();
            dto.ContentType = resource.ContentType;
            dto.File = System.Convert.FromBase64String(resource.File);
            return dto;
        }

        public async Task<CustomResource> CreateAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var base64 = System.Convert.ToBase64String(memoryStream.ToArray());
                return CustomResource.Create(file.ContentType, base64);
            }
        }
    }
}
