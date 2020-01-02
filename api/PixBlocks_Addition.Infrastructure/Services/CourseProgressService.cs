using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class CourseProgressService
    {
        private readonly ICourseProgressRepository _courseProgressRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseProgressService(ICourseProgressRepository courseProgressRepository, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseProgressRepository = courseProgressRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }
    }
}
