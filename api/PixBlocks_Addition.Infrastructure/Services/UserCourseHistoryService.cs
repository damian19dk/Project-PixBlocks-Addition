using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class UserCourseHistoryService : IUserCourseHistoryService
    {
        private readonly IUserCourseHistoryRepository _userCourseHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceRepository _resourceRepository;


        public UserCourseHistoryService(IUserCourseHistoryRepository userCourseHistoryRepository, IAutoMapperConfig config, 
            IUserRepository userRepository, ICourseRepository courseRepository, IResourceHandler resourceHandler, IResourceRepository resourceRepository)
        {
            _userCourseHistoryRepository = userCourseHistoryRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = config.Mapper;
            _resourceHandler = resourceHandler;
            _resourceRepository = resourceRepository;
        }

        public async Task AddHistoryAsync(Guid userId, Guid courseId)
        {
            var user = await _userRepository.GetAsync(userId);
            var course = await _courseRepository.GetAsync(courseId);
            if (user == null) throw new MyException(MyCodesNumbers.WrongUserId, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.UserNotFound);
            if (course == null) throw new MyException(MyCodesNumbers.MissingCourse, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.CourseNotFound);



            var history = await _userCourseHistoryRepository.GetAllAsync(user);
            if (history == null) throw new MyException(MyCodesNumbers.MissingHistory, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.HistoryNotFound);

            foreach (Course check in history.Courses)
            {
                if (check.Id == courseId) throw new MyException(MyCodesNumbers.SameCourseInUserHistory, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.SameCourseInUserHistory);
            }
            history.Courses.Add(course);
            await _userCourseHistoryRepository.UpdateAsync(history);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new MyException(MyCodesNumbers.WrongUserId, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.UserNotFound);

            var Ids = await _userCourseHistoryRepository.GetAllAsync(user);
            if (Ids == null) throw new MyException(MyCodesNumbers.MissingHistory, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.HistoryNotFound);

            List<Course> result = new List<Course>();
            foreach (Course Id in Ids.Courses)
            {
                result.Add(Id);
            }
    
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new MyException(MyCodesNumbers.WrongUserId, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.UserNotFound);
            var result = await _userCourseHistoryRepository.GetAllAsync(user);
            await _userCourseHistoryRepository.RemoveAsync(user);
        }

        public async Task CleanUserHistory(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new MyException(MyCodesNumbers.WrongUserId, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.UserNotFound);
            var progres = await _userCourseHistoryRepository.GetAllAsync(user);

            progres.Courses.Clear();

            await _userCourseHistoryRepository.UpdateAsync(progres);
        }
    }
}
