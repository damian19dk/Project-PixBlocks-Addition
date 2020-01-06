using System;
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
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceRepository _resourceRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IChangeMediaHandler<Course, Course> _changeMediaHandler;
        private readonly IMapper _mapper;
        private readonly IJWPlayerService _jwPlayerService;
        private readonly ILocalizationService _localizer;

        public CourseService(ICourseRepository courseRepository, IVideoRepository videoRepository, IQuizRepository quizRepository,
            IChangeMediaHandler<Course, Course> changeMediaHandler, IResourceRepository imageRepository,
            IResourceHandler imageHandler, IAutoMapperConfig config, IJWPlayerService jwPlayerService, ILocalizationService localizer)
        {
            _changeMediaHandler = changeMediaHandler;
            _resourceHandler = imageHandler;
            _resourceRepository = imageRepository;
            _mapper = config.Mapper;
            _courseRepository = courseRepository;
            _videoRepository = videoRepository;
            _jwPlayerService = jwPlayerService;
            _quizRepository = quizRepository;
            _localizer = localizer;
        }

        public async Task CreateAsync(MediaResource resource)
        {
            if (resource.Title == null)
            {
                throw new MyException(MyCodesNumbers.InvalidTitle, MyCodes.EmptyTitle);
            }
            var courses = await _courseRepository.GetAsync(resource.Title, resource.Language);
            foreach (var c in courses)
            {
                if (c.Title == resource.Title)
                    throw new MyException(MyCodesNumbers.SameTitleCourse, $"Kurs o tytule: {resource.Title} już istnieje.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            if (!string.IsNullOrEmpty(resource.Tags))
            {
                var resourceTags = resource.Tags.Split(',', ';');
                foreach (string tag in resourceTags)
                    tags.Add(new Tag(tag));
            }

            HashSet<string> resources = new HashSet<string>();
            if (resource.Resources != null)
            {
                foreach (var file in resource.Resources)
                {
                    var res = await _resourceHandler.CreateAsync(file);
                    await _resourceRepository.AddAsync(res);
                    resources.Add(res.Id.ToString());
                }
            }

            if (resource.Image != null)
            {
                var img = await _resourceHandler.CreateAsync(resource.Image);
                await _resourceRepository.AddAsync(img);
                resource.PictureUrl = img.Id.ToString();
            }

            var course = new Course(resource.Premium, Guid.Empty, resource.Title, resource.Description,
                                    resource.PictureUrl, resource.Language, 0, resources, tags);
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateAsync(ChangeMediaResource resource)
        {
            await _changeMediaHandler.ChangeAsync(resource, _courseRepository);
        }

        public async Task<IEnumerable<CourseDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<CourseDto>>(await _courseRepository.GetAllByTagsAsync(tags, _localizer.Language));

        public async Task<IEnumerable<CourseDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _courseRepository.GetAllAsync(page, count, _localizer.Language);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task<CourseDto> GetAsync(Guid id)
        {
            var result = await tryGetCourseAsync(id);
            return _mapper.Map<Course, CourseDto>(result);
        }

        public async Task<IEnumerable<CourseDto>> GetAsync(string title)
        {
            var result = await _courseRepository.GetAsync(title, _localizer.Language);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
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

            if (courseVideo.Video.QuizId != null)
            {
                var quiz = await _quizRepository.GetAsync((Guid)courseVideo.Video.QuizId);
                if (quiz != null)
                    await _quizRepository.RemoveAsync(quiz);
            }
            await _courseRepository.UpdateAsync(course);
            await _videoRepository.RemoveAsync(videoId);
        }

        public async Task RemoveAsync(Guid id)
        {
            var course = await tryGetCourseAsync(id);
            if (course.CourseVideos != null && course.CourseVideos.Count > 0)
            {
                foreach(var vid in course.CourseVideos)
                {
                    await _jwPlayerService.DeleteVideoAsync(vid.Video.MediaId);
                    await _videoRepository.RemoveAsync(vid.Video.Id);
                }
                course.CourseVideos.Clear();
                await _courseRepository.UpdateAsync(course);
            }
            if (course.Tags != null && course.Tags.Count > 0)
            {
                await _courseRepository.RemoveAllTagsAsync(course);
            }

            if(course.QuizId != null)
            {
                var quiz = await _quizRepository.GetAsync((Guid)course.QuizId);
                await _quizRepository.RemoveAsync(quiz);
            }
            await _courseRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            var courses = await _courseRepository.GetAsync(title);
            if(courses.Count() > 1)
            {
                throw new MyException(MyCodesNumbers.AmbiguousTitle, $"Cannot remove course {title}. Ambiguous title.");
            }
            if(courses.Count() < 0 )
            {
                throw new MyException(MyCodesNumbers.CourseNotFound, $"Course with title {title} not found.");
            }

            var course = courses.First();
            if (course.QuizId != null)
            {
                var quiz = await _quizRepository.GetAsync((Guid)course.QuizId);
                await _quizRepository.RemoveAsync(quiz);
            }
            await _courseRepository.RemoveAsync(course.Id);
        }
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
                throw new MyException(MyCodesNumbers.CourseNotFound, $"Nie znaleziono kursu o id: {id}. Wpierw stwórz kurs!");
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
                    var imageFromDb = await _resourceRepository.GetAsync(id);
                    if (imageFromDb != null)
                        await _resourceRepository.RemoveAsync(id);
                }
            }
        }

        public async Task<int> CountAsync()
        {
            return await _courseRepository.CountAsync(_localizer.Language);
        }
    }
}
