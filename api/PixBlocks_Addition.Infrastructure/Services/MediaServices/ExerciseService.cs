using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services.MediaServices
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ILessonRepository _lessonRepository;

        public ExerciseService(IExerciseRepository exerciseRepository, IVideoRepository videoRepository, 
                               ILessonRepository lessonRepository)
        {
            _exerciseRepository = exerciseRepository;
            _videoRepository = videoRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task AddVideoAsync(UploadResource upload)
        {
            var video = await _videoRepository.GetByMediaAsync(upload.MediaId);
            if (video == null)
            {
                throw new Exception($"Video with mediaId {video.MediaId} not found. Create the video first.");
            }

            var exercise = await _exerciseRepository.GetAsync(upload.ParentName);
            if (exercise == null)
            {
                throw new Exception($"Exercise with title {upload.ParentName} not found. Create the exercise first.");
            }

            var sameVideo = exercise.ExerciseVideos.FirstOrDefault(c => c.Video.MediaId == upload.MediaId);
            if (sameVideo != null)
            {
                throw new Exception($"The exercise already has the same video.");
            }

            exercise.ExerciseVideos.Add(new ExerciseVideo(exercise.Id, video));
            await _exerciseRepository.UpdateAsync(exercise);
        }

        public async Task CreateAsync(MediaResource resource)
        {
            var lesson = await _lessonRepository.GetAsync(resource.ParentName);
            var exercises = lesson.Exercises;
            var exerciseInCourse = exercises.FirstOrDefault(c => c.Title == resource.Title);
            if (exerciseInCourse != null)
            {
                throw new Exception($"Exercise with title {resource.Title} already exists in the lesson.");
            }

            HashSet<Tag> tags = new HashSet<Tag>();
            foreach (string tag in resource.Tags)
                tags.Add(new Tag(tag));

            var exercise = new Exercise(lesson, string.Empty, resource.Premium, resource.Title, resource.Description,
                                    resource.Picture, 0, resource.Language, tags);
            await _exerciseRepository.AddAsync(exercise);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync()
        {
            var result = await _exerciseRepository.GetAllAsync();
            return Mappers.AutoMapperConfig.Mapper.Map<IEnumerable<ExerciseDto>>(result);
        }

        public async Task<ExerciseDto> GetAsync(Guid id)
        {
            var result = await _exerciseRepository.GetAsync(id);
            return Mappers.AutoMapperConfig.Mapper.Map<ExerciseDto>(result);
        }

        public async Task<ExerciseDto> GetAsync(string title)
        {
            var result = await _exerciseRepository.GetAsync(title);
            return Mappers.AutoMapperConfig.Mapper.Map<ExerciseDto>(result);
        }

        public async Task RemoveAsync(Guid id)
        {
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
                throw new Exception("Exercise not found.");
            }
            var exerciseVideo = exercise.ExerciseVideos.SingleOrDefault(x => x.Video.Id == videoId);
            if (exerciseVideo == null)
            {
                throw new Exception("Video not found.");
            }
            exercise.ExerciseVideos.Remove(exerciseVideo);
            await _exerciseRepository.UpdateAsync(exercise);
        }
    }
}
