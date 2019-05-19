using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public CourseDto course { get; set; }
        public string VideoSrc { get; set; }
    }
}
