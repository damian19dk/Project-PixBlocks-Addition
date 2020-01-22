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
        private readonly ITagRepository _tagRepository;

        public ChangeMediaHandler(IResourceHandler resourceHandler, IResourceRepository resourceRepository, ITagRepository tagRepository)
        {
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
            _tagRepository = tagRepository;
        }

        public async Task ChangeAsync(ChangeMediaResource resource, IMediaRepository<TEntity> mediaRepository, IMediaRepository<TParent> parentRepository = null)
        {
            var entity = await mediaRepository.GetAsync(resource.Id);
            if (entity == null)
            {
                if (parentRepository == null)
                    throw new MyException(MyCodesNumbers.MediaNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
                else
                    throw new MyException(MyCodesNumbers.MediaNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoNotFound);
            }
            if (entity.Title != resource.Title)
            {
                IEnumerable<Media> sameTitle;
                if (parentRepository == null)
                    sameTitle = await mediaRepository.GetAsync(resource.Title, resource.Language);
                else
                    sameTitle = await parentRepository.GetAsync(resource.Title, resource.Language);

                if (sameTitle != null && sameTitle.Count() > 0)
                {
                    if (parentRepository == null)
                        throw new MyException(MyCodesNumbers.SameTitleCourse, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseTitleTaken);
                    else
                        throw new MyException(MyCodesNumbers.SameVideo, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoTitleTaken);
                }
                entity.SetTitle(resource.Title);
            }
            if (resource.Description != entity.Description)
                entity.SetDescription(resource.Description);
            if (resource.Language != entity.Language)
                entity.SetLanguage(resource.Language);
            if (resource.Premium != entity.Premium)
                entity.SetPremium(resource.Premium);
            
            if (string.IsNullOrEmpty(resource.Tags))
            {
                entity.Tags.Clear();
            }
            else
            {
                var tags = resource.Tags.Split(',', ';');

                ICollection<Tag> tagsToRemove = new HashSet<Tag>();
                foreach(var entityTag in entity.Tags)
                {
                    if(!tags.Contains(entityTag.Name))
                    {
                        tagsToRemove.Add(entityTag);
                    }
                }
                foreach (var tagEntity in tagsToRemove)
                    entity.Tags.Remove(tagEntity);
                
                foreach(var tag in tags)
                {
                    var tagToAdd = await _tagRepository.GetAsync(tag, resource.Language);
                    if(tagToAdd == null)
                    {
                        throw new MyException(MyCodesNumbers.TagNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.TagNotFound);
                    }
                    if(!entity.Tags.Contains(tagToAdd))
                    {
                        entity.Tags.Add(tagToAdd);
                    }
                }
            }

            if(resource.Resources != null)
            {
                //remove resources
                foreach(var res in entity.Resources)
                {
                    await tryRemoveResourceFromDb(res);
                }
                //add resources
                foreach(var file in resource.Resources)
                {
                    var res = await _resourceHandler.CreateAsync(file);
                    await _resourceRepository.AddAsync(res);
                    entity.Resources.Add(res.Id.ToString());
                }
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
