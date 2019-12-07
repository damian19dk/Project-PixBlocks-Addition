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
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceRepository _resourceRepository;

        public ChangeMediaHandler(IResourceHandler resourceHandler, IResourceRepository resourceRepository)
        {
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
        }

        public async Task ChangeAsync(ChangeMediaResource resource, IMediaRepository<TEntity> mediaRepository, IMediaRepository<TParent> parentRepository = null)
        {
            var entity = await mediaRepository.GetAsync(resource.Id);
            if (entity == null)
            {
                throw new MyException(MyCodesNumbers.MediaNotFound, $"Nie znaleziono media o id: {resource.Id}. Wpierw stwórz media");
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
                    throw new MyException(MyCodesNumbers.SameTitleMedia, $"Istnieje już media o tytule: {resource.Title}.");
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
                await tryRemoveResourceFromDb(entity.Picture);
                entity.SetPicture(resource.PictureUrl);
            }
            //Add picture to database
            if (resource.Image != null)
            {
                await tryRemoveResourceFromDb(entity.Picture);
                var img = await _resourceHandler.CreateAsync(resource.Image);
                await _resourceRepository.AddAsync(img);
                entity.SetPicture(img.Id.ToString());
            }

            await mediaRepository.UpdateAsync(entity);
        }

        private async Task tryRemoveResourceFromDb(string resource)
        {
            //Remove picture from database
            if (!string.IsNullOrWhiteSpace(resource))
            {
                Guid id;
                if (Guid.TryParse(resource, out id))
                {
                    var resourceFromDb = await _resourceRepository.GetAsync(id);
                    if (resourceFromDb != null)
                        await _resourceRepository.RemoveAsync(id);
                }
            }
        }
    }
}
