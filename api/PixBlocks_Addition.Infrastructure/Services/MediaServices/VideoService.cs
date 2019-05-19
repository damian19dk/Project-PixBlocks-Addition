using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IJWPlayerService _jwPlayerService;

        public VideoService(IVideoRepository videoRepository, IJWPlayerService jwPlayerService)
        {
            _videoRepository = videoRepository;
            _jwPlayerService = jwPlayerService;
        }

        public async Task AddAsync(MediaResource video)
        {
            var videoFromDatabase = await _videoRepository.GetByMediaAsync(video.MediaId);
            if (videoFromDatabase != null)
            {
                throw new Exception($"The video with mediaId: {video.MediaId} already exists.");
            }
            var response = await _jwPlayerService.GetVideoAsync(video.MediaId);

            var picture = string.IsNullOrWhiteSpace(video.Picture) ? getPicture(video.MediaId) : video.Picture;

            HashSet<Tag> tags = new HashSet<Tag>();
            if (video.Tags != null)
            {
                foreach (string tag in video.Tags)
                    tags.Add(new Tag(tag));
            }
            else
            {
                tags = null;
            }
            var vid = new Video(video.MediaId, video.Premium, video.Title, video.Description, picture,
                                response.Duration, video.Language, tags);

            await _videoRepository.AddAsync(vid);
            await _videoRepository.UpdateAsync(vid);
        }

        public async Task<IEnumerable<VideoDto>> GetAllAsync()
        {
            var result = await _videoRepository.GetAllAsync();
            return Mappers.AutoMapperConfig.Mapper.Map<IEnumerable<Video>, IEnumerable<VideoDto>>(result);
        }

        public async Task<VideoDto> GetAsync(Guid id)
        {
            var video = await _videoRepository.GetAsync(id);
            return Mappers.AutoMapperConfig.Mapper.Map<Video, VideoDto>(video);
        }

        public async Task<VideoDto> GetAsync(string title)
        {
            var video = await _videoRepository.GetAsync(title);
            return Mappers.AutoMapperConfig.Mapper.Map<Video, VideoDto>(video);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _videoRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            await _videoRepository.RemoveAsync(title);
        }

        private string getPicture(string mediaId)
            => "https://cdn.jwplayer.com/thumbs/" + mediaId + "-480.jpg";
    }
}
