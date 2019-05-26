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
                cfg.CreateMap<Tag, string>().ConvertUsing(x => x.Name);
                cfg.CreateMap<Video, VideoDto>().ForMember(x=>x.Picture,
                    opt=>opt.ConvertUsing(new PictureUrlConverter(_settings)));
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Media, MediaDto>().ForMember(x => x.Picture,
                    opt => opt.ConvertUsing(new PictureUrlConverter(_settings)));
                cfg.CreateMap<CourseVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<LessonVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<ExerciseVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<Course, CourseDto>().ForMember(x => x.Picture,
                    opt => opt.ConvertUsing(new PictureUrlConverter(_settings)));
                cfg.CreateMap<Lesson, LessonDto>().ForMember(x => x.Picture,
                    opt => opt.ConvertUsing(new PictureUrlConverter(_settings)));
                cfg.CreateMap<Exercise, ExerciseDto>().ForMember(x => x.Picture,
                    opt => opt.ConvertUsing(new PictureUrlConverter(_settings)));
            })
            .CreateMapper();
    }
}