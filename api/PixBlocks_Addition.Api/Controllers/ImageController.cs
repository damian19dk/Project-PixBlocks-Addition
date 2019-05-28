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
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<FileResult> GetImage(Guid id)
        {
            var image = await _imageService.GetAsync(id);
            return base.File(image.Image, image.ContentType);
        }

        [HttpPost]
        public async Task Upload()
        {
            var image = getImage();
            await _imageService.UploadAsync(image);
        }

        [HttpDelete("{id}")]
        public async Task Remove(Guid id)
        {
            await _imageService.RemoveAsync(id);
        }

        [HttpPost("{id}")]
        public async Task Change(Guid id)
        {
            var image = getImage();
            await _imageService.ChangeAsync(new ChangeImageResource() { Id = id, ImageFile = image });
        }

        private IFormFile getImage()
        {
            var image = Request.Form.Files.FirstOrDefault();
            if (image == null)
            {
                throw new MyException(MyCodesNumbers.ImageNotFound, MyCodes.ImageNotFound);
            }
            return image;
        }
    }
}