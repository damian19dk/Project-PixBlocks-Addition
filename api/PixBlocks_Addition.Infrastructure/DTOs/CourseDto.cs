﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class CourseDto: MediaDto
    {
        public ICollection<VideoDto> CourseVideos { get; set; }
    }
}
