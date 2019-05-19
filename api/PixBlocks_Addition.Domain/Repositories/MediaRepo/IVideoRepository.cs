using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories.MediaRepo
{
    public interface IVideoRepository: IMediaRepository<Video>
    {
        Task<Video> GetByMediaAsync(string mediaId);
    }
}
