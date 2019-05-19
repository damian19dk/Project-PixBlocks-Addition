using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Exercise: Media
    {
        public Lesson Lesson { get; protected set; }
        public ICollection<ExerciseVideo> ExerciseVideos { get; protected set; } = new HashSet<ExerciseVideo>();

        public Exercise(Lesson lesson, string mediaId, bool premium, string title, string description, string picture, 
            long duration, string lang, IEnumerable<Tag> tags = null)
            : base(mediaId, premium, title, description, picture, duration, lang, tags)
        {
            Lesson = lesson;
        }

        protected Exercise()
        {
        }
    }
}
