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
    public class ImageService : IImageService
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageHandler imageHandler, IImageRepository imageRepository)
        {
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
        }

        public async Task ChangeAsync(ChangeImageResource image)
        {
            var current = await getAsync(image.Id);

            var newImg = await _imageHandler.CreateAsync(image.ImageFile);

            current.SetImage(newImg.ContentType, newImg.Image);
            await _imageRepository.UpdateAsync(current);
        }

        public async Task<ImageDto> GetAsync(Guid id)
        {
            var image = await getAsync(id);
            return _imageHandler.Convert(image);
        }

        public async Task RemoveAsync(Guid id)
        {
            await getAsync(id);
            await _imageRepository.RemoveAsync(id);
        }

        public async Task UploadAsync(IFormFile image)
        {
            var img = await _imageHandler.CreateAsync(image);
            await _imageRepository.AddAsync(img);
        }

        private async Task<CustomImage> getAsync(Guid id)
        {
            var image = await _imageRepository.GetAsync(id);
            if (image == null)
            {
                throw new MyException(MyCodesNumbers.ImageNotFound, $"Nie znaleziono zdjęcia z id: {id}.");
            }
            return image;
        }
    }
}
