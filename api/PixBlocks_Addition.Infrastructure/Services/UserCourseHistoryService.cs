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
            if (_userRepository.GetAsync(userId) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            if (_courseRepository.GetAsync(courseId) == null) throw new MyException(MyCodesNumbers.MissingCourse, MyCodes.MissingCourse);

            var course = await _courseRepository.GetAsync(courseId);
    
            var userCourseHistory = new UserCourseHistory(userId, course);

            await _userCourseHistoryRepository.AddAsync(userCourseHistory);
        }

        public async Task AddHistoryAsync(string login, Guid courseId)
        {
            if (_userRepository.GetAsync(login) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            if (_courseRepository.GetAsync(courseId) == null) throw new MyException(MyCodesNumbers.MissingCourse, MyCodes.MissingCourse);

            var user = await _userRepository.GetAsync(login);

            var course = await _courseRepository.GetAsync(courseId);

            var userCourseHistory = new UserCourseHistory(user.Id, course);

            await _userCourseHistoryRepository.AddAsync(userCourseHistory);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync(Guid userId)
        {
            if (_userRepository.GetAsync(userId) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);

            var Ids = await _userCourseHistoryRepository.GetAllAsync(userId);

            List<Course> result = new List<Course>();
            foreach (Course Id in Ids.Courses)
            {
                result.Add(Id);
            }
            if (result.Count == 0) throw new MyException(MyCodesNumbers.MissingHistory, MyCodes.MissingHistory);

            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync(string login)
        {
            if (_userRepository.GetAsync(login) == null) throw new MyException(MyCodesNumbers.MissingUser, MyCodes.MissingUser);

            var user = await _userRepository.GetAsync(login);

            var Ids = await _userCourseHistoryRepository.GetAllAsync(user.Id);

            List<Course> result = new List<Course>();
            foreach (Course Id in Ids.Courses)
            {
                result.Add(Id);
            }
            if (result.Count == 0) throw new MyException(MyCodesNumbers.MissingHistory, MyCodes.MissingHistory);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(result);
        }

        public async Task RemoveAsync(Guid userId)
        {
            if (_userRepository.GetAsync(userId) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            var result = await _userCourseHistoryRepository.GetAllAsync(userId);
            await _userCourseHistoryRepository.RemoveAsync(userId);
        }

        public async Task RemoveAsync(string login)
        {
            if (_userRepository.GetAsync(login) == null) throw new MyException(MyCodesNumbers.MissingUser, MyCodes.MissingUser);
            var result = await _userCourseHistoryRepository.GetAllAsync(login);
            var user = await _userRepository.GetAsync(login);
            
            await _userCourseHistoryRepository.RemoveAsync(login);
        }
    }
}
