using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<FileResult> GetResource(Guid id)
        {
            var resource = await _resourceService.GetAsync(id);
            return base.File(resource.File, resource.ContentType);
        }

        [HttpPost]
        public async Task Upload()
        {
            var resource = getResource();
            await _resourceService.UploadAsync(resource);
        }

        [HttpDelete("{id}")]
        public async Task Remove(Guid id)
        {
            await _resourceService.RemoveAsync(id);
        }

        [HttpPost("{id}")]
        public async Task Change(Guid id)
        {
            var resource = getResource();
            await _resourceService.ChangeAsync(new ChangeResource() { Id = id, ResourceFile = resource });
        }

        private IFormFile getResource()
        {
            var resource = Request.Form.Files.FirstOrDefault();
            if (resource == null)
            {
                throw new MyException(MyCodesNumbers.ImageNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.ImageNotFound);
            }
            return resource;
        }
    }
}