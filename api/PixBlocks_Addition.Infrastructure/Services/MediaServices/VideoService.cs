using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class VideoService : IVideoService
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IJWPlayerService _jwPlayerService;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IJWPlayerService jwPlayerService, 
                            IImageHandler imageHandler, IImageRepository imageRepository, IAutoMapperConfig config)
        {
            _mapper = config.Mapper;
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
            _videoRepository = videoRepository;
            _jwPlayerService = jwPlayerService;
        }

        public async Task CreateAsync(MediaResource video)
        {
            var videoFromDatabase = await _videoRepository.GetByMediaAsync(video.MediaId);
            if (videoFromDatabase != null)
            {
                throw new MyException($"The video with mediaId: {video.MediaId} already exists.");
            }
            var response = await _jwPlayerService.GetVideoAsync(video.MediaId);
            
            if (video.Image != null)
            {
                var img = await _imageHandler.CreateAsync(video.Image);
                await _imageRepository.AddAsync(img);
                video.PictureUrl = img.Id.ToString();
            }

            var picture = string.IsNullOrWhiteSpace(video.PictureUrl) ? getPicture(video.MediaId) : video.PictureUrl;

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
            return _mapper.Map<IEnumerable<Video>, IEnumerable<VideoDto>>(result);
        }

        public async Task<IEnumerable<VideoDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _videoRepository.GetAllAsync(page, count);
            return _mapper.Map<IEnumerable<Video>, IEnumerable<VideoDto>>(result);
        }

        public async Task<VideoDto> GetAsync(Guid id)
        {
            var video = await _videoRepository.GetAsync(id);
            return _mapper.Map<Video, VideoDto>(video);
        }

        public async Task<VideoDto> GetAsync(string mediaId)
        {
            var video = await _videoRepository.GetByMediaAsync(mediaId);
            return _mapper.Map<VideoDto>(video);
        }

        public async Task<IEnumerable<VideoDto>> BrowseAsync(string title)
        {
            var videos = await _videoRepository.GetAsync(title);
            return _mapper.Map<IEnumerable<VideoDto>>(videos);
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
