using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Lesson: Media
    {
        public Course Course { get; protected set; }
        public ICollection<LessonVideo> LessonVideos { get; protected set; } = new HashSet<LessonVideo>();
        public ICollection<Exercise> Exercises { get; protected set; } = new HashSet<Exercise>();

        public Lesson(Course course, string mediaId, bool premium, string title, string description, string picture, long duration,
            string lang, IEnumerable<Tag> tags = null)
            : base(mediaId, premium, title, description, picture, duration, lang, tags)
        {
            Course = course;
        }

        protected Lesson()
        {
        }
    }
}
