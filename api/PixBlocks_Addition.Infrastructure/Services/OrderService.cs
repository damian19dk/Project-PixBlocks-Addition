using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ILocalizationService _localizationService;

        public OrderService(ICourseRepository courseRepository, IVideoRepository videoRepository, ILocalizationService localizationService)
        {
            _courseRepository = courseRepository;
            _localizationService = localizationService;
            _videoRepository = videoRepository;
        }

        public async Task OrderCourses(IEnumerable<Guid> courses)
        {
            var count = await _courseRepository.CountAsync(_localizationService.Language);
            if(courses.Count() != count)
            {
                throw new MyException(MyCodesNumbers.InvalidOrderData, "The given collection size is incorrect.");
            }
            List<Course> validCourses = new List<Course>();
            var index = 1;
            foreach(var id in courses)
            {
                var course = await _courseRepository.GetAsync(id);
                if(course == null || !string.Equals(course.Language, _localizationService.Language, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new MyException(MyCodesNumbers.CourseNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
                }
                course.SetIndex(index);
                validCourses.Add(course);
                index++;
            }

            await _courseRepository.UpdateAsync(validCourses);
        }

        public async Task OrderVideos(IEnumerable<Guid> videos, Guid courseId)
        {
            var course = await _courseRepository.GetAsync(courseId);
            if (course == null || !string.Equals(course.Language, _localizationService.Language, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);
            }

            var count = course.CourseVideos.Count;
            if (videos.Count() != count)
            {
                throw new MyException(MyCodesNumbers.InvalidOrderData, "The given collection size is incorrect.");
            }
            List<Video> validVideos = new List<Video>();
            var index = 1;
            foreach (var id in videos)
            {
                if(course.CourseVideos.SingleOrDefault(x=>x.Video.Id==id) == null)
                {
                    throw new MyException(MyCodesNumbers.InvalidOrderData, $"Video with id {id} does not belong to the given course.");
                }
                var video = await _videoRepository.GetAsync(id);
                if (video == null)
                {
                    throw new MyException(MyCodesNumbers.VideoNotFound, $"Video with id {id} does not exist");
                }
                video.SetIndex(index);
                validVideos.Add(video);
                index++;
            }

            await _videoRepository.UpdateAsync(validVideos);
        }
    }
}
