using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            string file = $"Resources\\MyExceptions.{_localizationService.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var count = await _courseRepository.CountAsync(_localizationService.Language);
            if(courses.Count() != count)
            {
                throw new MyException(MyCodesNumbers.InvalidOrderData, doc.SelectSingleNode($"exceptions/InvalidOrderData").InnerText);
            }
            List<Course> validCourses = new List<Course>();
            var index = 1;
            foreach(var id in courses)
            {
                var course = await _courseRepository.GetAsync(id);
                if(course == null || !string.Equals(course.Language, _localizationService.Language, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_localizationService.Language == "en")
                        throw new MyException(MyCodesNumbers.CourseNotFound, $"Course with id {id} does not exist!");
                    else
                        throw new MyException(MyCodesNumbers.CourseNotFound, $"Kurs z id {id} nie istnieje!");
                }
                course.SetIndex(index);
                validCourses.Add(course);
                index++;
            }

            await _courseRepository.UpdateAsync(validCourses);
        }

        public async Task OrderVideos(IEnumerable<Guid> videos, Guid courseId)
        {
            string file = $"Resources\\MyExceptions.{_localizationService.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var course = await _courseRepository.GetAsync(courseId);
            if (course == null || !string.Equals(course.Language, _localizationService.Language, StringComparison.InvariantCultureIgnoreCase))
            {
                if (_localizationService.Language == "en")
                    throw new MyException(MyCodesNumbers.CourseNotFound, $"Course with id {courseId} does not exist!");
                else
                    throw new MyException(MyCodesNumbers.CourseNotFound, $"Kurs z id {courseId} nie istnieje!");
            }

            var count = course.CourseVideos.Count;
            if (videos.Count() != count)
            {
                throw new MyException(MyCodesNumbers.InvalidOrderData, doc.SelectSingleNode($"exceptions/InvalidOrderData").InnerText);
            }
            List<Video> validVideos = new List<Video>();
            var index = 1;
            foreach (var id in videos)
            {
                if(course.CourseVideos.SingleOrDefault(x=>x.Video.Id==id) == null)
                {
                    if (_localizationService.Language == "en")
                        throw new MyException(MyCodesNumbers.InvalidOrderData, $"Video with id {id} does not belong to the given course!");
                    else
                        throw new MyException(MyCodesNumbers.InvalidOrderData, $"Wideo z id {id} nie należy do danego kursu!");

                }
                var video = await _videoRepository.GetAsync(id);
                if (video == null)
                {
                    if (_localizationService.Language == "en")
                        throw new MyException(MyCodesNumbers.VideoNotFound, $"Video with id {id} does not exist!");
                    else
                        throw new MyException(MyCodesNumbers.VideoNotFound, $"Nie istnieje wideo z id {id}!");
                }
                video.SetIndex(index);
                validVideos.Add(video);
                index++;
            }

            await _videoRepository.UpdateAsync(validVideos);
        }
    }
}
