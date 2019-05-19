using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public interface ILessonService: IMediaService<LessonDto, MediaResource>
    {
        Task RemoveVideoFromLessonAsync(Guid lessonId, Guid videoId);
    }
}
