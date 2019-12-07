using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IImageHandler _imageHandler;
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IImageHandler imageHandler, IResourceRepository resourceRepository)
        {
            _imageHandler = imageHandler;
            _resourceRepository = resourceRepository;
        }

        public async Task ChangeAsync(ChangeResource resource)
        {
            var currentResource = await getAsync(resource.Id);

            var newResource = await _imageHandler.CreateAsync(resource.ResourceFile);

            currentResource.SetFile(newResource.ContentType, newResource.File);
            await _resourceRepository.UpdateAsync(currentResource);
        }

        public async Task<ImageDto> GetAsync(Guid id)
        {
            var resource = await getAsync(id);
            return _imageHandler.Convert(resource);
        }

        public async Task RemoveAsync(Guid id)
        {
            await getAsync(id);
            await _resourceRepository.RemoveAsync(id);
        }

        public async Task UploadAsync(IFormFile file)
        {
            var resource = await _imageHandler.CreateAsync(file);
            await _resourceRepository.AddAsync(resource);
        }

        private async Task<CustomResource> getAsync(Guid id)
        {
            var resource = await _resourceRepository.GetAsync(id);
            if (resource == null)
            {
                throw new MyException(MyCodesNumbers.ImageNotFound, $"Nie znaleziono zasobu z id: {id}.");
            }
            return resource;
        }
    }
}
