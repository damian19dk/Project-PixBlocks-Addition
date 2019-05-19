using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class LessonVideo
    {
        public Guid Id { get; protected set; }
        public Guid LessonId { get; protected set; }
        public Video Video { get; protected set; }

        public LessonVideo(Guid lessonId, Video video)
        {
            LessonId = lessonId;
            Video = video;
        }

        protected LessonVideo()
        {
        }
    }
}
