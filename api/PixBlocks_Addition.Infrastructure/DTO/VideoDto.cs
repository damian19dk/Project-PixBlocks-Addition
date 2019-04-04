using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTO
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string VideoSrc { get; set; }
    }
}
