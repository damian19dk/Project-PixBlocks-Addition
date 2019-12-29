using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class UserCourseHistoryDto
    {
        public User User { get; set; }
        public CourseDto Course { get; set; }
    }
}
