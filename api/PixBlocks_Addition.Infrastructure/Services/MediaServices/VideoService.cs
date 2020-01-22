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
        private readonly IQuizRepository _quizRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILocalizationService _localizer;
        private readonly IJWPlayerService _jwPlayerService;
        private readonly IChangeMediaHandler<Video, Video> _changeMediaHandler;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, ICourseRepository courseRepository, IJWPlayerService jwPlayerService, 
                            IResourceHandler resourceHandler, IResourceRepository resourceRepository, ILocalizationService localizer,
                            IChangeMediaHandler<Video, Video> changeMediaHandler, IAutoMapperConfig config, IQuizRepository quizRepository,
                            ITagRepository tagRepository)
        {
            _changeMediaHandler = changeMediaHandler;
            _mapper = config.Mapper;
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
            _quizRepository = quizRepository;
            _localizer = localizer;
            _videoRepository = videoRepository;
            _courseRepository = courseRepository;
            _tagRepository = tagRepository;
            _jwPlayerService = jwPlayerService;
        }

        public async Task CreateAsync(CreateVideoResource video)
        {
            if(video.ParentId == null)
            {
                throw new MyException(MyCodesNumbers.InvalidVideoParentId, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
            }
            var course = await _courseRepository.GetAsync(video.ParentId);
            if(course == null)
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
            }

            if (!string.IsNullOrEmpty(video.MediaId))
            {
                var sameVideo = course.CourseVideos.FirstOrDefault(c => c.Video.MediaId == video.MediaId);
                if (sameVideo != null)
                {
                    throw new MyException(MyCodesNumbers.SameVideoInCourse, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoAlreadyExists);
                }

                var response = await _jwPlayerService.GetVideoAsync(video.MediaId);
                if(response == null)
                {
                    throw new MyException(MyCodesNumbers.VideoNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoNotFound);
                }
            }
            else
            {
                if(video.Video == null)
                {
                    throw new MyException(MyCodesNumbers.MissingFile, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.MissingVideoFile);
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
                {
                    var entityTag = await _tagRepository.GetAsync(tag, video.Language);
                    if(entityTag == null)
                    {
                        throw new MyException(MyCodesNumbers.TagNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.TagNotFound);
                    }
                    tags.Add(entityTag);
                }
            }
            

                var vid = new Video(video.MediaId, video.ParentId, video.Premium, video.Title, video.Description, picture,
                                    0, video.Language, resources, tags);

            vid.SetStatus("processing");
            await _videoRepository.AddAsync(vid);

            course.AddTimeToDuration(video.Duration);
            course.CourseVideos.Add(new CourseVideo(video.ParentId, vid));
            await _courseRepository.UpdateAsync(course);

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
            if (video == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoNotFound);
            }

            course.TakeTimeFromDuration(video.Duration);
            await _courseRepository.UpdateAsync(course);

            if (video.QuizId != null)
            {
                var quiz = await _quizRepository.GetAsync((Guid)video.QuizId);
                await _quizRepository.RemoveAsync(quiz);
            }
            await _videoRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            var videos = await _videoRepository.GetAsync(title);
            if (videos.Count() > 1)
            {
                throw new MyException(MyCodesNumbers.AmbiguousTitle, $"Video with title {title} is ambiguous.");
            }
            if(videos.Count() == 0)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoNotFound);
            }
        
            var video = videos.First();
            var course = await _courseRepository.GetAsync(video.ParentId);

            course.TakeTimeFromDuration(video.Duration);
            await _courseRepository.UpdateAsync(course);

            if (video.QuizId != null)
            {
                var quiz = await _quizRepository.GetAsync((Guid)video.QuizId);
                await _quizRepository.RemoveAsync(quiz);
            }
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

        public async Task ChangeCourse(ChangeAttachedCourseResource attachCourse)
        {
            var newCourse = await _courseRepository.GetAsync(attachCourse.CourseId);
            if(newCourse == null)
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
            }
            var video = await _videoRepository.GetAsync(attachCourse.VideoId);
            if(video == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoNotFound);
            }
            var sameName = newCourse.CourseVideos.Any(x => x.Video.Title == video.Title);
            if(sameName)
            {
                throw new MyException(MyCodesNumbers.SameVideoInCourse, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.VideoTitleTaken);
            }

            var currentCourse = await _courseRepository.GetAsync(video.ParentId);
            var courseVideo = currentCourse.CourseVideos.SingleOrDefault(x => x.Video.Id == video.Id);
            currentCourse.CourseVideos.Remove(courseVideo);
            await _courseRepository.UpdateAsync(currentCourse);

            video.SetParent(newCourse);
            newCourse.CourseVideos.Add(new CourseVideo(newCourse.Id, video));

            await _videoRepository.UpdateAsync(video);
            await _courseRepository.UpdateAsync(newCourse);
        }
    }
}
