using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Mappers
{
    public class AutoMapperConfig: IAutoMapperConfig
    {
        private readonly IOptions<HostOptions> _settings;

        public AutoMapperConfig(IOptions<HostOptions> settings)
        {
            _settings = settings;
        }

        public IMapper Mapper
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuizAnswer, QuizAnswerDto>();
                cfg.CreateMap<QuizQuestion, QuizQuestionDto>();
                cfg.CreateMap<Quiz, QuizDto>();
                cfg.CreateMap<Tag, TagDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Video, VideoDto>()
                    .ForMember(x => x.Picture, opt => opt.ConvertUsing(new PictureUrlConverter(_settings)))
                    .ForMember(x => x.Resources, opt => opt.ConvertUsing(new ResourceUrlConverter(_settings)));
                cfg.CreateMap<Media, MediaDto>()
                    .ForMember(x => x.Picture, opt => opt.ConvertUsing(new PictureUrlConverter(_settings)))
                    .ForMember(x => x.Resources, opt => opt.ConvertUsing(new ResourceUrlConverter(_settings)));
                cfg.CreateMap<CourseVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<Course, CourseDto>()
                    .ForMember(x => x.Picture, opt => opt.ConvertUsing(new PictureUrlConverter(_settings)))
                    .ForMember(x => x.Resources, opt => opt.ConvertUsing(new ResourceUrlConverter(_settings)));
                cfg.CreateMap<VideoRecord, VideoRecordDto>();
                cfg.CreateMap<VideoHistory, VideoHistoryDto>();
            })
            .CreateMapper();
    }
}