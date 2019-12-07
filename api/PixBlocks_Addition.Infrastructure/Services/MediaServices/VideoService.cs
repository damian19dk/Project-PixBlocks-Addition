using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IResourceHandler _imageHandler;
        private readonly IResourceRepository _imageRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IJWPlayerService _jwPlayerService;
        private readonly IChangeMediaHandler<Video, Video> _changeMediaHandler;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IJWPlayerService jwPlayerService, 
                            IResourceHandler imageHandler, IResourceRepository imageRepository,
                            IChangeMediaHandler<Video, Video> changeMediaHandler, IAutoMapperConfig config)
        {
            _changeMediaHandler = changeMediaHandler;
            _mapper = config.Mapper;
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
            _videoRepository = videoRepository;
            _jwPlayerService = jwPlayerService;
        }

        public async Task CreateAsync(CreateVideoResource video)
        {
            if (!string.IsNullOrEmpty(video.MediaId))
            {
                var videoFromDatabase = await _videoRepository.GetByMediaAsync(video.MediaId);
                if (videoFromDatabase != null)
                {
                    throw new MyException(MyCodesNumbers.SameVideo, $"Wideo o id: {video.MediaId} istnieje!");
                }
                var response = await _jwPlayerService.GetVideoAsync(video.MediaId);
                if(response == null)
                {
                    throw new MyException(MyCodesNumbers.VideoNotFound, $"Nie znaleziono wideo o id:{video.MediaId}!");
                }
            }
            else
            {
                if(video.Video == null)
                {
                    throw new MyException(MyCodesNumbers.MissingFile, "Nie znaleziono wideo!");
                }

                video.MediaId = await _jwPlayerService.UploadVideoAsync(video.Video);
            }

            if (video.Image != null)
            {
                var img = await _imageHandler.CreateAsync(video.Image);
                await _imageRepository.AddAsync(img);
                video.PictureUrl = img.Id.ToString();
            }

            var picture = string.IsNullOrWhiteSpace(video.PictureUrl) ? getPicture(video.MediaId) : video.PictureUrl;

            HashSet<Tag> tags = new HashSet<Tag>();
            if (!string.IsNullOrEmpty(video.Tags))
            {
                var videoTags = video.Tags.Split(',', ';');
                foreach (string tag in videoTags)
                    tags.Add(new Tag(tag));
            }

            var vid = new Video(video.MediaId, video.Premium, video.Title, video.Description, picture,
                                0, video.Language, tags);
            vid.SetStatus("processing");
            await _videoRepository.AddAsync(vid);
            setDuration(video.MediaId, vid, (VideoRepository)_videoRepository.Clone());
        }

        private async Task setDuration(string mediaId, Video video, IVideoRepository videoRepository)
        {
            var response = await _jwPlayerService.ShowVideoAsync(mediaId);
            while (response.Status == "processing")
            {
                await Task.Delay(TimeSpan.FromSeconds(35));
                try
                {
                    response = await _jwPlayerService.ShowVideoAsync(mediaId);
                }
                catch (Exception e)
                {
                    video.SetStatus("Unknown");
                    await videoRepository.UpdateAsync(video);
                    throw e;
                }
            }
            video.SetStatus(response.Status);
            video.SetDuration((long)response.Duration);
            await videoRepository.UpdateAsync(video);
        }

        public async Task<IEnumerable<VideoDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<VideoDto>>(await _videoRepository.GetAllByTagsAsync(tags));

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

        public async Task<VideoDto> GetByMediaIdAsync(string mediaId)
        {
            var video = await _videoRepository.GetByMediaAsync(mediaId);
            return _mapper.Map<VideoDto>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetAsync(string title)
            => _mapper.Map<IEnumerable<VideoDto>>(await _videoRepository.GetAsync(title));

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

        public Task AddVideoAsync(UploadResource upload)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ChangeMediaResource resource)
            => await _changeMediaHandler.ChangeAsync(resource, _videoRepository);

        public async Task<int> CountAsync()
        {
            return await _videoRepository.CountAsync();
        }
    }
}
