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
    public class VideoHistoryService : IVideoHistoryService
    {
        private readonly IVideoHistoryRepository _videoHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoHistoryHelperRepository _videoHistoryHelperRepository;

        public VideoHistoryService(IVideoHistoryRepository videoHistoryRepository, IUserRepository userRepository, 
            IVideoRepository videoRepository, IAutoMapperConfig config, ICourseRepository courseRepository,
            IVideoHistoryHelperRepository videoHistoryHelperRepository)
        {
            _videoHistoryRepository = videoHistoryRepository;
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _mapper = config.Mapper;
            _courseRepository = courseRepository;
            _videoHistoryHelperRepository = videoHistoryHelperRepository;
        }

        public async Task AddAsync(Guid userId, Guid videoId, long time = 0)
        {
            var user = await _userRepository.GetAsync(userId);
            var video = await _videoRepository.GetAsync(videoId);
            var videoHistory = await _videoHistoryRepository.GetAsync(user);
            if (user == null) throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);
            if (video == null) throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);
            if (videoHistory == null) throw new MyException(MyCodesNumbers.VideoHistoryNotFound, MyCodes.VideoHistoryNotFound);
            if (time < 0) throw new MyException(MyCodesNumbers.NegativeTime, MyCodes.NegativeTime);

            foreach (var i in videoHistory.Videos)
            {
                if (i.Video == video) throw new MyException(MyCodesNumbers.VideoInHistory, MyCodes.VideoInHistory);
            }
            var helper = new VideoRecord(video, time);

            
            await _videoHistoryHelperRepository.AddAsync(helper);
            videoHistory.Videos.Add(helper);

        }

        public async Task UpdateAsync(Guid userId, Guid videoId, long time = 0)
        {
            var user = await _userRepository.GetAsync(userId);
            var video = await _videoRepository.GetAsync(videoId);
            var videoHistory = await _videoHistoryRepository.GetAsync(user);
            if (user == null) throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);
            if (video == null) throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);
            if (videoHistory == null) throw new MyException(MyCodesNumbers.VideoHistoryNotFound, MyCodes.VideoHistoryNotFound);
            if (time < 0) throw new MyException(MyCodesNumbers.NegativeTime, MyCodes.NegativeTime);

            long helper = 0;
            foreach(var i in videoHistory.Videos)
            {
                if (i.Video == video)
                    helper = i.Time;
            }

            videoHistory.Videos.Remove(new VideoRecord(video, helper));

            videoHistory.Videos.Add(new VideoRecord(video, time));

            //await _videoHistoryHelperRepository.UpdateAsync(videoHistory.Videos);
            await _videoHistoryRepository.UpdateAsync(videoHistory);
        }

        public async Task<VideoHistoryDto> GetUserVideoHistory(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);

            var result = await _videoHistoryRepository.GetAsync(user);

            return _mapper.Map<VideoHistory, VideoHistoryDto>(result);
        }

        public async Task<int> GetUserProgres(Guid userId, Guid courseId)
        {
            var user = await _userRepository.GetAsync(userId);
            var videoHistory = await _videoHistoryRepository.GetAsync(user);
            var course = await _courseRepository.GetAsync(courseId);
            if (user == null) throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);
            if (videoHistory == null) throw new MyException(MyCodesNumbers.VideoHistoryNotFound, MyCodes.VideoHistoryNotFound);
            if (course == null) throw new MyException(MyCodesNumbers.CourseNotFound, MyCodes.CourseNotFound);

            long time = 0;
            foreach(var i in videoHistory.Videos)
            {
               if (i.Video.ParentId == courseId)
                    time += i.Time;
            }
            int percentage = (int)(time / course.Length) * 100;
            return percentage;
        }
    }
}
