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
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceRepository _resourceRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILocalizationService _localizer;
        private readonly IJWPlayerService _jwPlayerService;
        private readonly IChangeMediaHandler<Video, Video> _changeMediaHandler;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, ICourseRepository courseRepository, IJWPlayerService jwPlayerService, 
                            IResourceHandler resourceHandler, IResourceRepository resourceRepository, ILocalizationService localizer,
                            IChangeMediaHandler<Video, Video> changeMediaHandler, IAutoMapperConfig config)
        {
            _changeMediaHandler = changeMediaHandler;
            _mapper = config.Mapper;
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
            _localizer = localizer;
            _videoRepository = videoRepository;
            _courseRepository = courseRepository;
            _jwPlayerService = jwPlayerService;
        }

        public async Task CreateAsync(CreateVideoResource video)
        {
            if(video.ParentId == null)
            {
                throw new MyException(MyCodesNumbers.InvalidVideoParentId, "You have to provide valid course id");
            }
            var course = await _courseRepository.GetAsync(video.ParentId);
            if(course == null)
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, "The given course id does not exist");
            }

            if (!string.IsNullOrEmpty(video.MediaId))
            {
                var sameVideo = course.CourseVideos.FirstOrDefault(c => c.Video.MediaId == video.MediaId);
                if (sameVideo != null)
                {
                    throw new MyException(MyCodesNumbers.SameVideoInCourse, MyCodes.SameVideoInCourse);
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

            HashSet<string> resources = new HashSet<string>();
            if (video.Resources != null)
            {
                foreach (var file in video.Resources)
                {
                    var res = await _resourceHandler.CreateAsync(file);
                    await _resourceRepository.AddAsync(res);
                    resources.Add(res.Id.ToString());
                }
            }

            if (video.Image != null)
            {
                var img = await _resourceHandler.CreateAsync(video.Image);
                await _resourceRepository.AddAsync(img);
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
            

                var vid = new Video(video.MediaId, video.ParentId, video.Premium, video.Title, video.Description, picture,
                                    0, video.Language, resources, tags);

            vid.SetStatus("processing");
            await _videoRepository.AddAsync(vid);

            course.CourseVideos.Add(new CourseVideo(video.ParentId, vid));
            await _courseRepository.UpdateAsync(course);

            setDuration(video.MediaId, vid, course, (VideoRepository)_videoRepository.Clone(), _courseRepository);
        }

        private async Task setDuration(string mediaId, Video video, Course course, IVideoRepository videoRepository, ICourseRepository courseRepository)
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

        private async Task setLength(Course course, long time, ICourseRepository courseRepository)
        {
            course.AddTime(time);
            //await courseRepository.UpdateAsync(course);
        }

        private async Task<long> TakeTime(string mediaId)
        {
            var video = await _jwPlayerService.GetVideoAsync(mediaId);
            return video.Duration;
        }

        public async Task<IEnumerable<VideoDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<VideoDto>>(await _videoRepository.GetAllByTagsAsync(tags, _localizer.Language));

        public async Task<IEnumerable<VideoDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _videoRepository.GetAllAsync(page, count, _localizer.Language);
            return _mapper.Map<IEnumerable<Video>, IEnumerable<VideoDto>>(result);
        }

        public async Task<VideoDto> GetAsync(Guid id)
        {
            var video = await _videoRepository.GetAsync(id);
            return _mapper.Map<Video, VideoDto>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetByMediaIdAsync(string mediaId)
        {
            var video = await _videoRepository.GetByMediaAsync(mediaId);
            return _mapper.Map<IEnumerable<VideoDto>>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetAsync(string title)
            => _mapper.Map<IEnumerable<VideoDto>>(await _videoRepository.GetAsync(title, _localizer.Language));

        public async Task RemoveAsync(Guid id)
        {
            var video = await _videoRepository.GetAsync(id);
            var course = await _courseRepository.GetAsync(video.ParentId);

            course.TakeTime(video.Duration);
            await _courseRepository.UpdateAsync(course);

            await _videoRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            var videos = await _videoRepository.GetAsync(title, string.Empty);
            var video = videos.SingleOrDefault(x => x.Title == title);
            var course = await _courseRepository.GetAsync(video.ParentId);

            course.TakeTime(video.Duration);
            await _courseRepository.UpdateAsync(course);

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
            return await _videoRepository.CountAsync(_localizer.Language);
        }
    }
}
