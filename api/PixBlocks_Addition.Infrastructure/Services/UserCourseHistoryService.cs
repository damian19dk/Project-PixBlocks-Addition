using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
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


        public UserCourseHistoryService(IUserCourseHistoryRepository userCourseHistoryRepository, IAutoMapperConfig config, 
            IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _userCourseHistoryRepository = userCourseHistoryRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = config.Mapper;
        }

        public async Task AddHistoryAsync(Guid userId, Course course)
        {
            if (_userRepository.GetAsync(userId) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            if (_courseRepository.GetAsync(course.Id) == null) throw new MyException(MyCodesNumbers.MissingCourse, MyCodes.MissingCourse);

            var userCourseHistory = new UserCourseHistory(userId, course);

            await _userCourseHistoryRepository.AddAsync(userCourseHistory);
        }

        public async Task AddHistoryAsync(string login, Course course)
        {
            if (_userRepository.GetAsync(login) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            if (_courseRepository.GetAsync(course.Id) == null) throw new MyException(MyCodesNumbers.MissingCourse, MyCodes.MissingCourse);

            var user = await _userRepository.GetAsync(login);

            var userCourseHistory = new UserCourseHistory(user.Id, course);

            await _userCourseHistoryRepository.AddAsync(userCourseHistory);
        }

        public async Task<IEnumerable<UserCourseHistoryDto>> GetAllAsync(Guid userId)
        {
            if (_userRepository.GetAsync(userId) == null) throw new MyException(MyCodesNumbers.WrongUserId, MyCodes.WrongUserId);
            var result = await _userCourseHistoryRepository.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<UserCourseHistory>, IEnumerable<UserCourseHistoryDto>>(result);
        }

        public async Task<IEnumerable<UserCourseHistoryDto>> GetAllAsync(string login)
        {
            if (_userRepository.GetAsync(login) == null) throw new MyException(MyCodesNumbers.MissingUser, MyCodes.MissingUser);
            var result = await _userCourseHistoryRepository.GetAllAsync(login);
            return _mapper.Map<IEnumerable<UserCourseHistory>, IEnumerable<UserCourseHistoryDto>>(result);
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
            await _userCourseHistoryRepository.RemoveAsync(login);
        }
    }
}
