using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class ExerciseVideo
    {
        public Guid Id { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public Video Video { get; protected set; }

        public ExerciseVideo(Guid exerciseId, Video video)
        {
            ExerciseId = exerciseId;
            Video = video;
        }

        protected ExerciseVideo()
        {
        }
    }
}
