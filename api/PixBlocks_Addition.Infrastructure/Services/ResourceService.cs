﻿using System;
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
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceHandler resourceHandler, IResourceRepository resourceRepository)
        {
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
        }

        public async Task ChangeAsync(ChangeResource resource)
        {
            var currentResource = await getAsync(resource.Id);

            var newResource = await _resourceHandler.CreateAsync(resource.ResourceFile);

            currentResource.SetFile(newResource.ContentType, newResource.File);
            await _resourceRepository.UpdateAsync(currentResource);
        }

        public async Task<ResourceDto> GetAsync(Guid id)
        {
            var resource = await getAsync(id);
            return _resourceHandler.Convert(resource);
        }

        public async Task RemoveAsync(Guid id)
        {
            await getAsync(id);
            await _resourceRepository.RemoveAsync(id);
        }

        public async Task UploadAsync(IFormFile file)
        {
            var resource = await _resourceHandler.CreateAsync(file);
            await _resourceRepository.AddAsync(resource);
        }

        private async Task<CustomResource> getAsync(Guid id)
        {
            var resource = await _resourceRepository.GetAsync(id);
            if (resource == null)
            {
                throw new MyException(MyCodesNumbers.ImageNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.ResourceNotFound);
            }
            return resource;
        }
    }
}
