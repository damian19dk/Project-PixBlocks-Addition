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
        public User User { get; protected set; }
        public ICollection<Course> Courses { get; protected set; } = new HashSet<Course>();
        public int TimeWatched { get; protected set; }
        public int PercentageWatched { get; protected set; } 

        public CourseProgress(User user, Course course, int timeWatched = 0, int percentedWatched = 0)
        {
            Id = Guid.NewGuid();
            User = user;
            Courses.Add(course);
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
