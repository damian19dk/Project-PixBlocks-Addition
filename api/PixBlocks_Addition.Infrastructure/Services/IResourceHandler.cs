using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IResourceHandler
    {
        Task<CustomResource> CreateAsync(IFormFile file);
        ResourceDto Convert(CustomResource resource);
    }
}
