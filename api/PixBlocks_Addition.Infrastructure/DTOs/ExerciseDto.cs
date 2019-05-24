using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class ExerciseDto: MediaDto
    {
        public ICollection<VideoDto> ExerciseVideos { get; set; }
    }
}
