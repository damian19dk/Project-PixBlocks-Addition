using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public interface IChangeMediaHandler<TEntity> where TEntity: Media
    {
        Task ChangeAsync(ChangeMediaResource resource, IMediaRepository<TEntity> mediaRepository, IMediaRepository<TEntity> parentRepository = null);
    }
}
