using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tag, string>().ConvertUsing(x => x.Name);
                var vidMapping = cfg.CreateMap<Video, VideoDto>();
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Media, MediaDto>();
                cfg.CreateMap<CourseVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<LessonVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<ExerciseVideo, VideoDto>().ConvertUsing(x => Mapper.Map<VideoDto>(x.Video));
                cfg.CreateMap<Course, CourseDto>();
                cfg.CreateMap<Lesson, LessonDto>();
                cfg.CreateMap<Exercise, ExerciseDto>();
            })
            .CreateMapper();
    }
}