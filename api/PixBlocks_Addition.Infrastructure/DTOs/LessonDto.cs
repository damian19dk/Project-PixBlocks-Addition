using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class LessonDto: MediaDto
    {
        public ICollection<VideoDto> LessonVideos { get; set; }
        public ICollection<ExerciseDto> Exercises { get; set; }
    }
}
