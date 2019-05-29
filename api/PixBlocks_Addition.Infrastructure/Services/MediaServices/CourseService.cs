﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IChangeMediaHandler<Course, Course> _changeMediaHandler;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IVideoRepository videoRepository,
            IChangeMediaHandler<Course, Course> changeMediaHandler, IImageRepository imageRepository,
            IImageHandler imageHandler, IAutoMapperConfig config)
        {
            _changeMediaHandler = changeMediaHandler;
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
            _mapper = config.Mapper;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if (video == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, $"Nie znaleziono wideo o MediaId: {video.MediaId}. Wpierw stwórz wideo.");
            }

            var course = await tryGetCourseAsync(upload.ParentId);

            var sameVideo = course.CourseVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if (sameVideo != null)
            {
                throw new MyException(MyCodesNumbers.SameVideo, MyCodes.SameVideoInCourse);
            }

            course.CourseVideos.Add(new CourseVideo(course.Id, video));
            await _courseRepository.UpdateAsync(course);
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var c = await _courseRepository.GetAsync(resource.Title);
            if (c.Count() > 0)
            {
                throw new MyException(MyCodesNumbers.SameTitleCourse, $"Kurs o tytule: {resource.Title} już istnieje.");
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

            var course = new Course(resource.Premium, resource.Title, resource.Description,
                                    resource.PictureUrl, resource.Language, 0, tags);
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateAsync(ChangeMediaResource resource)
        {
            await _changeMediaHandler.ChangeAsync(resource, _courseRepository);
        }

        public async Task<IEnumerable<CourseDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<CourseDto>>(await _courseRepository.GetAllByTagsAsync(tags));

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var result = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _courseRepository.GetAllAsync(page, count);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task<CourseDto> GetAsync(Guid id)
        {
            var result = await tryGetCourseAsync(id);
            return _mapper.Map<Course, CourseDto>(result);
        }

        public async Task<IEnumerable<CourseDto>> GetAsync(string title)
        {
            var result = await _courseRepository.GetAsync(title);
            return _mapper.Map<IEnumerable<CourseDto>>(result);
        }

        public async Task RemoveVideoFromCourseAsync(Guid courseId, Guid videoId)
        {
            var course = await tryGetCourseAsync(courseId);
            var courseVideo = course.CourseVideos.SingleOrDefault(x => x.Video.Id == videoId);
            if (courseVideo == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);
            }
            course.CourseVideos.Remove(courseVideo);
            await _courseRepository.UpdateAsync(course);
        }

        public async Task RemoveAsync(Guid id)
            => await _courseRepository.RemoveAsync(id);

        public async Task RemoveAsync(string title)
            => await _courseRepository.RemoveAsync(title);

        /// <summary>
        /// Gets course or throws an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Course> tryGetCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetAsync(id);
            if (course == null)
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, $"Course with id {id} not found. Create the course first.");
            }
            return course;
        }

        private async Task tryRemoveImageFromDb(string coursePicture)
        {
            //Remove picture from database
            if (!string.IsNullOrWhiteSpace(coursePicture))
            {
                Guid id;
                if (Guid.TryParse(coursePicture, out id))
                {
                    var imageFromDb = await _imageRepository.GetAsync(id);
                    if (imageFromDb != null)
                        await _imageRepository.RemoveAsync(id);
                }
            }
        }
    }
}
