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
    public class ExerciseService : IExerciseService
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageRepository _imageRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IChangeMediaHandler<Exercise, Lesson> _changeMediaHandler;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IVideoRepository videoRepository,
                               ILessonRepository lessonRepository, IImageHandler imageHandler,
                               IImageRepository imageRepository, IAutoMapperConfig config,
                               IChangeMediaHandler<Exercise, Lesson> changeMediaHandler)
        {
            _mapper = config.Mapper;
            _changeMediaHandler = changeMediaHandler;
            _imageHandler = imageHandler;
            _imageRepository = imageRepository;
            _exerciseRepository = exerciseRepository;
            _videoRepository = videoRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if (video == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, $"Nie znaleziono wideo o MediaId: {video.MediaId}. Wpierw stwórz wideo.");
            }

            var exercise = await _exerciseRepository.GetAsync(upload.ParentId);
            if (exercise == null)
            {
                throw new MyException(MyCodesNumbers.ExerciseNotFound, $"Nie znaleziono ćwiczenia o id: {upload.ParentId}. Wpierw stwórz ćwiczenie.");
            }

            var sameVideo = exercise.ExerciseVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if (sameVideo != null)
            {
                throw new MyException(MyCodesNumbers.SameVideoInExercise, MyCodes.SameVideoInExercise);
            }

            exercise.ExerciseVideos.Add(new ExerciseVideo(exercise.Id, video));
            await _exerciseRepository.UpdateAsync(exercise);
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var lesson = await _lessonRepository.GetAsync(resource.ParentId);
            if (lesson == null)
            {
                throw new MyException(MyCodesNumbers.LessonNotFound, $"Lesson {resource.ParentId} not found.");
            }
            var exercises = lesson.Exercises;
            var exerciseInCourse = exercises.FirstOrDefault(c => c.Title == resource.Title);
            if (exerciseInCourse != null)
            {
                throw new MyException(MyCodesNumbers.SameTitleExercise, $"Ćwiczenie o tytule: {resource.Title} już istnieje w lekcji.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            if (!string.IsNullOrEmpty(resource.Tags))
            {
                var resourceTags = resource.Tags.Split(',', ';');
                foreach (string tag in resourceTags)
                    tags.Add(new Tag(tag));
            }

            if (resource.Image != null)
            {
                var img = await _imageHandler.CreateAsync(resource.Image);
                await _imageRepository.AddAsync(img);
                resource.PictureUrl = img.Id.ToString();
            }

            var exercise = new Exercise(lesson, string.Empty, resource.Premium, resource.Title, resource.Description,
                                    resource.PictureUrl, 0, resource.Language, tags);
            await _exerciseRepository.AddAsync(exercise);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllByTagsAsync(IEnumerable<string> tags)
            => _mapper.Map<IEnumerable<ExerciseDto>>(await _exerciseRepository.GetAllByTagsAsync(tags));

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync(int page, int count = 10)
        {
            var result = await _exerciseRepository.GetAllAsync(page, count);
            return _mapper.Map<IEnumerable<ExerciseDto>>(result);
        }

        public async Task<ExerciseDto> GetAsync(Guid id)
        {
            var result = await _exerciseRepository.GetAsync(id);
            return _mapper.Map<ExerciseDto>(result);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAsync(string title)
        {
            var result = await _exerciseRepository.GetAsync(title);
            return _mapper.Map<IEnumerable<ExerciseDto>>(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            var exercise = await _exerciseRepository.GetAsync(id);
            if (exercise == null)
            {
                throw new MyException(MyCodes.ExerciseNotFound, $"Nie znaleziono ćwiczenia o id {id}.");
            }
            if (exercise.ExerciseVideos!= null && exercise.ExerciseVideos.Count > 0)
            {
                exercise.ExerciseVideos.Clear();
                await _exerciseRepository.UpdateAsync(exercise);
            }
            if (exercise.Tags != null && exercise.Tags.Count > 0)
            {
                await _exerciseRepository.RemoveAllTagsAsync(exercise);
            }
            await _exerciseRepository.RemoveAsync(id);
        }

        public async Task RemoveAsync(string title)
        {
            await _exerciseRepository.RemoveAsync(title);
        }

        public async Task RemoveVideoFromExerciseAsync(Guid exerciseId, Guid videoId)
        {
            var exercise = await _exerciseRepository.GetAsync(exerciseId);
            if (exercise == null)
            {
                throw new MyException(MyCodesNumbers.ExerciseNotFound, MyCodes.ExerciseNotFound);
            }
            var exerciseVideo = exercise.ExerciseVideos.SingleOrDefault(x => x.Video.Id == videoId);
            if (exerciseVideo == null)
            {
                throw new MyException(MyCodesNumbers.VideoNotFound, MyCodes.VideoNotFound);
            }
            exercise.ExerciseVideos.Remove(exerciseVideo);
            await _exerciseRepository.UpdateAsync(exercise);
        }

        public async Task UpdateAsync(ChangeMediaResource resource)
            => await _changeMediaHandler.ChangeAsync(resource, _exerciseRepository, _lessonRepository);

    }
}
