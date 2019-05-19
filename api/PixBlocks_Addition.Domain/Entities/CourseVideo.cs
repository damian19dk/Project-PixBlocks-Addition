using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class CourseVideo
    {
        public Guid Id { get; protected set; }
        public Video Video { get; protected set; }
        public Guid CourseId { get; protected set; }

        public CourseVideo(Guid courseId, Video video)
        {
            CourseId = courseId;
            Video = video;
        }

        protected CourseVideo()
        {
        }
    }
}
