﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;

        public CourseService(ICourseRepository courseRepository, IVideoRepository videoRepository)
        {
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if (video == null)
            {
                throw new Exception($"Video with mediaId {video.MediaId} not found. Create the video first.");
            }

            var course = await _courseRepository.GetAsync(upload.ParentName);
            if (course == null)
            {
                throw new Exception($"Course with title {upload.ParentName} not found. Create the course first.");
            }

            var sameVideo = course.CourseVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if (sameVideo != null)
            {
                throw new Exception($"The course already has the same video.");
            }

            course.CourseVideos.Add(new CourseVideo(course.Id, video));
            await _courseRepository.UpdateAsync(course);
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var c = await _courseRepository.GetAsync(resource.Title);
            if(c!=null)
            {
                throw new Exception($"The course with title {resource.Title} already exists.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            if (resource.Tags != null)
            {
                foreach (string tag in resource.Tags)
                    tags.Add(new Tag(tag));
            }else
            {
                tags = null;
            }
            var course = new Course(resource.Premium, resource.Title, resource.Picture, 
                                    resource.Language, resource.Language, 0, tags);
            await _courseRepository.AddAsync(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var result = await _courseRepository.GetAllAsync();
            return Mappers.AutoMapperConfig.Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task<CourseDto> GetAsync(Guid id)
        {
            var result = await _courseRepository.GetAsync(id);
            return Mappers.AutoMapperConfig.Mapper.Map<Course, CourseDto>(result);
        }

        public async Task<CourseDto> GetAsync(string title)
        {
            var result = await _courseRepository.GetAsync(title);
            return Mappers.AutoMapperConfig.Mapper.Map<Course, CourseDto>(result);
        }

        public async Task RemoveVideoFromCourseAsync(Guid courseId, Guid videoId)
        {
            var course = await _courseRepository.GetAsync(courseId);
            if(course == null)
            {
                throw new Exception("Course not found.");
            }
            var courseVideo = course.CourseVideos.SingleOrDefault(x => x.Video.Id == videoId);
            if(courseVideo == null)
            {
                throw new Exception("Video not found.");
            }
            course.CourseVideos.Remove(courseVideo);
            await _courseRepository.UpdateAsync(course);
        }

        public async Task RemoveAsync(Guid id)
            => await _courseRepository.RemoveAsync(id);

        public async Task RemoveAsync(string title)
            => await _courseRepository.RemoveAsync(title);
    }
}