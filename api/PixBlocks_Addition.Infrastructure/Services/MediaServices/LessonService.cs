using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LessonService : ILessonService
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IChangeMediaHandler<Lesson, Course> _changeMediaHandler;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository, 
                             IVideoRepository videoRepository, IImageHandler imageHandler, 
                             IImageRepository imageRepository, IAutoMapperConfig config,
                             IChangeMediaHandler<Lesson, Course> changeMediaHandler)
        {
            _changeMediaHandler = changeMediaHandler;
            _mapper = config.Mapper;
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var course = await _courseRepository.GetAsync(resource.ParentId);
            if(course==null)
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, $"Course {resource.ParentId} not found.");
            }
            var lessons = course.Lessons;
            var lessonInCourse = lessons.FirstOrDefault(c => c.Title == resource.Title);
            if(lessonInCourse!=null)
            {
                throw new MyException(MyCodesNumbers.SameTitleLesson, $"Lekcja o tytule: {resource.Title} już istnieje.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            if (resource.Tags != null)
            {
                resource.Tags = resource.Tags.First().Split();
                foreach (string tag in resource.Tags)
                    tags.Add(new Tag(tag));
            }
            else
            {
                tags = null;
            }

            if (resource.Image != null)
            {
                var img = await _imageHandler.CreateAsync(resource.Image);
                await _imageRepository.AddAsync(img);
                resource.PictureUrl = img.Id.ToString();
            }

            var lesson = new Lesson(course, string.Empty, resource.Premium, resource.Title, resource.Description, 
                                    resource.PictureUrl, 0, resource.Language, tags);
            await _lessonRepository.AddAsync(lesson);
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if(video == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, $"Nie znaleziono wideo o MediaId: {video.MediaId}. Wpierw stwórz wideo.");
            }

            var lesson = await _lessonRepository.GetAsync(upload.ParentId);
            if(lesson == null)
            {
                throw new MyException(MyCodesNumbers.LessonNotFound, $"Nie znaleziono lekcji o id: {upload.ParentId}. Wpierw stwórz lekcję.");
            }

            var sameVideo = lesson.LessonVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if(sameVideo != null)
            {
                throw new MyException(MyCodesNumbers.SameVideo, MyCodes.SameVideo);
            }

            lesson.LessonVideos.Add(new LessonVideo(lesson.Id, video));
            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task<IEnumerable<LessonDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<LessonDto>>(await _lessonRepository.GetAllByTagsAsync(tags));

        public async Task<IEnumerable<LessonDto>> GetAllAsync()
        {
            var result = await _lessonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LessonDto>>(result);
        }

        public async Task<IEnumerable<LessonDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _lessonRepository.GetAllAsync(page, count);
            return _mapper.Map<IEnumerable<LessonDto>>(result);
        }

        public async Task<LessonDto> GetAsync(Guid id)
        {
            var result = await _lessonRepository.GetAsync(id);
            return _mapper.Map<LessonDto>(result);
        }

        public async Task<IEnumerable<LessonDto>> GetAsync(string title)
        {
            var result = await _lessonRepository.GetAsync(title);
            return _mapper.Map<IEnumerable<LessonDto>>(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _lessonRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            await _lessonRepository.RemoveAsync(title);
        }

        public async Task RemoveVideoFromLessonAsync(Guid lessonId, Guid videoId)
        {
            var lesson = await _lessonRepository.GetAsync(lessonId);
            if (lesson == null)
            {
                throw new MyException(MyCodesNumbers.LessonNotFound, MyCodes.LessonNotFound);
            }
            var lessonVideo = lesson.LessonVideos.SingleOrDefault(x => x.Video.Id == videoId);
            if (lessonVideo == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);
            }
            lesson.LessonVideos.Remove(lessonVideo);
            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task UpdateAsync(ChangeMediaResource resource)
            => await _changeMediaHandler.ChangeAsync(resource, _lessonRepository, _courseRepository);
    }
}
