using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;

        public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository, 
                             IVideoRepository videoRepository)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var course = await _courseRepository.GetAsync(resource.ParentName);
            var lessons = course.Lessons;
            var lessonInCourse = lessons.FirstOrDefault(c => c.Title == resource.Title);
            if(lessonInCourse!=null)
            {
                throw new MyException($"Lesson with title {resource.Title} already exists in the course.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            foreach (string tag in resource.Tags)
                tags.Add(new Tag(tag));

            var lesson = new Lesson(course, string.Empty, resource.Premium, resource.Title, resource.Description, 
                                    resource.Picture, 0, resource.Language, tags);
            await _lessonRepository.AddAsync(lesson);
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if(video == null)
            {
                throw new MyException($"Video with mediaId {video.MediaId} not found. Create the video first.");
            }
            var lesson = await _lessonRepository.GetAsync(upload.ParentName);
            if(lesson == null)
            {
                throw new MyException($"Lesson with title {upload.ParentName} not found. Create the lesson first.");
            }

            var sameVideo = lesson.LessonVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if(sameVideo!=null)
            {
                throw new MyException(MyCodesNumbers.SameVideo, MyCodes.SameVideo);
            }

            lesson.LessonVideos.Add(new LessonVideo(lesson.Id, video));
            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task<IEnumerable<LessonDto>> GetAllAsync()
        {
            var result = await _lessonRepository.GetAllAsync();
            return Mappers.AutoMapperConfig.Mapper.Map<IEnumerable<LessonDto>>(result);
        }

        public async Task<LessonDto> GetAsync(Guid id)
        {
            var result = await _lessonRepository.GetAsync(id);
            return Mappers.AutoMapperConfig.Mapper.Map<LessonDto>(result);
        }

        public async Task<LessonDto> GetAsync(string title)
        {
            var result = await _lessonRepository.GetAsync(title);
            return Mappers.AutoMapperConfig.Mapper.Map<LessonDto>(result);
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
    }
}
