using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class ChangeMediaHandler<TEntity, TParent>: IChangeMediaHandler<TEntity, TParent> where TEntity : Media where TParent: Media
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;

        public ChangeMediaHandler(IImageHandler imageHandler, IImageRepository imageRepository)
        {
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
        }

        public async Task ChangeAsync(ChangeMediaResource resource, IMediaRepository<TEntity> mediaRepository, IMediaRepository<TParent> parentRepository = null)
        {
            var entity = await mediaRepository.GetAsync(resource.Id);
            if (entity == null)
            {
                throw new MyException($"Media with id {resource.Id} not found. Create the media first.");
            }
            if (entity.Title != resource.Title)
            {
                IEnumerable<Media> sameTitle;
                if (parentRepository == null)
                    sameTitle = await mediaRepository.GetAsync(resource.Title);
                else
                    sameTitle = await parentRepository.GetAsync(resource.Title);

                if (sameTitle != null && sameTitle.Count() > 0)
                {
                    throw new MyException($"There is already a media with title {resource.Title}.");
                }
                entity.SetTitle(resource.Title);
            }
            if (resource.Description != entity.Description)
                entity.SetDescription(resource.Description);
            if (resource.Language != entity.Language)
                entity.SetLanguage(resource.Language);
            if (resource.Premium != entity.Premium)
                entity.SetPremium(resource.Premium);
            
            if (!string.IsNullOrEmpty(resource.Tags))
            {
                var tags = resource.Tags.Split(',', ';');
                //Remove tags
                ISet<Tag> tagsToRemove = new HashSet<Tag>();
                foreach (var tag in entity.Tags)
                {
                    if (!tags.Contains(tag.Name))
                    {
                        tagsToRemove.Add(tag);
                    }
                }
                await mediaRepository.RemoveTagsAsync(entity, tagsToRemove);
                //Add tags
                foreach (var tag in tags)
                {
                    if (!entity.Tags.Any(t => t.Name == tag))
                    {
                        entity.Tags.Add(new Tag(tag));
                    }
                }
            }
            else
            {
                await mediaRepository.RemoveAllTagsAsync(entity);
            }

            //Add picture from url
            if (resource.PictureUrl != null)
            {
                await tryRemoveImageFromDb(entity.Picture);
                entity.SetPicture(resource.PictureUrl);
            }
            //Add picture to database
            if (resource.Image != null)
            {
                await tryRemoveImageFromDb(entity.Picture);
                var img = await _imageHandler.CreateAsync(resource.Image);
                await _imageRepository.AddAsync(img);
                entity.SetPicture(img.Id.ToString());
            }

            await mediaRepository.UpdateAsync(entity);
        }

        private async Task tryRemoveImageFromDb(string coursePicture)
        {
            //Remove picture from database
            if (!string.IsNullOrWhiteSpace(coursePicture))
            {
                Guid id;
                if (Guid.TryParse(coursePicture, out id))
                {
                    var imageFromDb = await _imageRepository.GetAsync(id);
                    if (imageFromDb != null)
                        await _imageRepository.RemoveAsync(id);
                }
            }
        }
    }
}
