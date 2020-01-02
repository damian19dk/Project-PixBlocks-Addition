using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Entities
{
    public class CourseProgress
    {
        private readonly ICourseRepository _courseRepository;

        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid CourseId { get; protected set; }
        public int TimeWatched { get; protected set; }
        public int PercentageWatched { get; protected set; } 

        public CourseProgress(Guid userId, Guid courseId, int timeWatched = 0, int percentedWatched = 0)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CourseId = courseId;
            TimeWatched = timeWatched;
            PercentageWatched = percentedWatched; 
        }

        protected CourseProgress()
        {

        }

        public void IncreseTime(int time)
        {
            
        }
    }
}
