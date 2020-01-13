﻿using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class VideoHistoryService : IVideoHistoryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly IVideoHistoryRepository _videoHistoryRepository;

        public VideoHistoryService(IUserRepository userRepository, IVideoRepository videoRepository, 
            IAutoMapperConfig config, ICourseRepository courseRepository,
            IVideoHistoryRepository videoHistoryRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _mapper = config.Mapper;
            _courseRepository = courseRepository;
            _videoHistoryRepository = videoHistoryRepository;
        }

        public async Task SetProgressAsync(Guid userId, Guid videoId, long time = 0)
        {
            if (time < 0)
                throw new MyException(MyCodesNumbers.NegativeTime, MyCodes.NegativeTime);

            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);

            var video = await _videoRepository.GetAsync(videoId);
            if (video == null)
                throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);

            var videoHistory = await _videoHistoryRepository.GetAsync(user);
            if (videoHistory == null)
                throw new MyException(MyCodesNumbers.VideoHistoryNotFound, MyCodes.VideoHistoryNotFound);

            VideoRecord videoRecord = videoHistory.Videos.SingleOrDefault(v => v.Video.Id == videoId);
            if(videoRecord == null)
            {
                videoRecord = new VideoRecord(video, time);
                videoHistory.Videos.Add(videoRecord);
            }
            else
            {
                videoRecord.SetTime(time);
            }

            await _videoHistoryRepository.UpdateAsync(videoHistory);
        }

        public async Task<VideoHistoryDto> GetHistoryAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);

            var result = await _videoHistoryRepository.GetAsync(user);

            return _mapper.Map<VideoHistory, VideoHistoryDto>(result);
        }

        public async Task<VideoRecordDto> GetVideoProgressAsync(Guid userId, Guid videoId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);

            var video = await _videoRepository.GetAsync(videoId);
            if (video == null)
                throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);

            var videoRecord = await _videoHistoryRepository.GetAsync(user, videoId);
            return _mapper.Map<VideoRecordDto>(videoRecord);
        }

        public async Task<int> GetProgressAsync(Guid userId, Guid courseId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                throw new MyException(MyCodesNumbers.UserNotFound, MyCodes.UserNotFound);

            var course = await _courseRepository.GetAsync(courseId);
            if (course == null)
                throw new MyException(MyCodesNumbers.CourseNotFound, MyCodes.CourseNotFound);

            var videoHistory = await _videoHistoryRepository.GetAsync(user);
            if (videoHistory == null)
                throw new MyException(MyCodesNumbers.VideoHistoryNotFound, MyCodes.VideoHistoryNotFound);

            var videos = await _videoRepository.GetVideosFromCourse(course);
            if (videos == null)
                throw new MyException(MyCodesNumbers.MissingVideos, MyCodes.MissingVideos);

            long time = 0;
            long length = 0;
            foreach(var record in videoHistory.Videos)
            {
               if (record.Video.ParentId == courseId)
                    time += record.Time;
            }

            foreach(var vid in videos)
            {
                length += vid.Duration;
            }

            double temp = (double)time / (double)length;
            return (int)(temp * 100);
        }
    }
}
